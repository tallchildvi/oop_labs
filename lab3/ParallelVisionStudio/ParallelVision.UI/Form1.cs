using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParallelVision.Core;

namespace ParallelVision.UI
{
    public partial class Form1 : Form
    {
        // Сховище для оброблених картинок, щоб переглядати їх за кліком
        private readonly Dictionary<string, (Bitmap Original, Bitmap Processed)> _datasetResult = new();

        public Form1()
        {
            InitializeComponent();
            SetupCustomStyles();
        }

        private void SetupCustomStyles()
        {
            // Переконайся, що ListBox реагує на кліки
            listBoxUrls.SelectedIndexChanged += ListBoxUrls_SelectedIndexChanged;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // 10 якісних URL-адрес для формування великого пулу даних
            string[] baseUrls = {
        "https://images.unsplash.com/photo-1579546929518-9e396f3cc809?w=1200",
        "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=1200",
        "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=1200",
        "https://images.unsplash.com/photo-1470071459604-3b5ec3a7fe05?w=1200",
        "https://images.unsplash.com/photo-1447752875215-b2761acb3c5d?w=1200",
        "https://images.unsplash.com/photo-1461749280684-dccba630e2f6?w=1200",
        "https://images.unsplash.com/photo-1513542789411-b6a5d4f31634?w=1200",
        "https://images.unsplash.com/photo-1506744038136-46273834b3fb?w=1200",
        "https://images.unsplash.com/photo-1518531933037-91b2f5f229cc?w=1200",
        "https://images.unsplash.com/photo-1502082553048-f009c37129b9?w=1200"
    };

            btnStart.Enabled = false;
            progressBar.Value = 10;
            lblTime.Text = "⏳ Етап 1: Кешування базових зображень з мережі...";

            var cachedImages = new List<Bitmap>();
            using var client = new System.Net.Http.HttpClient();

            foreach (var url in baseUrls)
            {
                try
                {
                    var data = await client.GetByteArrayAsync(url);
                    using var ms = new System.IO.MemoryStream(data);
                    cachedImages.Add(new Bitmap(new Bitmap(ms)));
                }
                catch { /* Ігноруємо помилки мережі для окремих файлів */ }
            }

            if (cachedImages.Count == 0)
            {
                MessageBox.Show("Не вдалося завантажити жодного зображення. Перевір інтернет.");
                btnStart.Enabled = true;
                return;
            }

            // 🧠 СТВОРЕННЯ ВЕЛИКОГО ДАТАСЕТУ (Множимо картинки в пам'яті)
            // Кожну завантажену картинку дублюємо 5 разів. Разом: ~50 важких зображень.
            var largeDataset = new List<Bitmap>();
            for (int i = 0; i < 5; i++)
            {
                foreach (var img in cachedImages)
                {
                    largeDataset.Add((Bitmap)img.Clone());
                }
            }

            progressBar.Value = 40;
            lblTime.Text = $"⏳ Етап 2: Обробка датасету ({largeDataset.Count} шт) на CPU...";

            int threads = (int)numericThreads.Value;

            // Вибір стратегії (паттерн Strategy)
            IImageProcessor processor = rbParallel.Checked ?
                new ParallelProcessor() : new SequentialProcessor();

            var processedImages = new List<Bitmap>();

            // 🔥 ЗАПУСК ТАЙМЕРА (Міряємо тільки чистий прорахунок)
            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var img in largeDataset)
            {
                // Відправляємо кожну картинку на розрив процесору
                var processed = await Task.Run(() => processor.Process(img, threads));
                processedImages.Add(processed);
            }

            watch.Stop();

            // 3. Виведення результатів
            progressBar.Value = 100;
            btnStart.Enabled = true;

            long elapsedMs = watch.ElapsedMilliseconds;
            lblTime.Text = $"[{processor.Name}] Потоків: {threads}. Оброблено: {largeDataset.Count} картинок за {elapsedMs} мс";

            // Оновлюємо PictureBox для візуалізації
            picOriginal.Image = largeDataset[0];
            picProcessed.Image = processedImages[0];
        }

        // Клік по елементу в списку показує його індивідуальний результат
        private void ListBoxUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUrls.SelectedItem is string selectedName && _datasetResult.ContainsKey(selectedName))
            {
                // Для наочності: ліворуч покажемо оригінал (просто реверс інверсії, щоб знову став нормальним)
                var processor = new SequentialProcessor();
                picOriginal.Image = processor.Process(_datasetResult[selectedName].Original, 1);

                // Праворуч — оброблений паралельно результат
                picProcessed.Image = _datasetResult[selectedName].Processed;
            }
        }
    }
}