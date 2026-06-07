using System;
using System.IO;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using ParallelVision.Core;

namespace ParallelVision.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // Тестові URL-адреси з гарантованими картинками
            string[] urls = {
                "https://images.unsplash.com/photo-1579546929518-9e396f3cc809?w=500", // Абстрактний градієнт
                "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=500"  // Нічне місто
            };

            int threads = (int)numericThreads.Value;

            // Ініціалізуємо наш паралельний процесор та конвеєр
            var processor = new ParallelProcessor();
            var pipeline = new ProcessingPipeline(processor, threads);

            // Налаштовуємо потокобезпечний прогрес-бар
            var progress = new Progress<int>(value => progressBar.Value = value);
            pipeline.ProgressChanged += value => ((IProgress<int>)progress).Report(value);

            btnStart.Enabled = false;
            lblTime.Text = "Завантаження та обробка...";

            // Покажемо першу картинку як оригінал перед стартом (для демонстрації викладачу)
            try
            {
                using var client = new HttpClient();
                byte[] originalBytes = await client.GetByteArrayAsync(urls[0]);
                using var ms = new MemoryStream(originalBytes);
                picOriginal.Image = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження оригіналу: {ex.Message}");
                btnStart.Enabled = true;
                return;
            }

            // Запуск бенчмарку
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Запускаємо конвеєр обробки у фоновому потоці
            await pipeline.StartAsync(urls);

            watch.Stop();
            btnStart.Enabled = true;
            lblTime.Text = $"Завершено за: {watch.ElapsedMilliseconds} мс";

            // Дістаємо оброблений результат із черги та виводимо на екран
            if (pipeline.OutputQueue.TryTake(out byte[] processedBytes))
            {
                try
                {
                    using var ms = new MemoryStream(processedBytes);
                    picProcessed.Image = Image.FromStream(ms);
                }
                catch
                {
                    lblTime.Text += " [Помилка конвертації результату]";
                }
            }
        }
    }
}