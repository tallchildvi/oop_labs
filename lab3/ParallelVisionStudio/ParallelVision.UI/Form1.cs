using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParallelVision.Core;

namespace ParallelVision.UI
{
    public partial class Form1 : Form
    {
        private readonly List<(string Name, Bitmap Original, Bitmap Processed)> _processedDataset = new();

        public Form1()
        {
            InitializeComponent();
            SetupDefaultSettings();
        }

        private void SetupDefaultSettings()
        {
            if (comboFilter.Items.Count > 0) comboFilter.SelectedIndex = 0;
            listBoxUrls.SelectedIndexChanged += ListBoxUrls_SelectedIndexChanged;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {

            string[] baseUrls = {
                "https://images.unsplash.com/photo-1579546929518-9e396f3cc809?w=800",
                "https://images.unsplash.com/photo-1533105079780-92b9be482077?w=800",
                "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=800",
                "https://images.unsplash.com/photo-1470071459604-3b5ec3a7fe05?w=800"
            };

            _processedDataset.Clear();
            listBoxUrls.Items.Clear();
            progressBar.Value = 0;
            btnStart.Enabled = false;
            lblTime.Text = "Крок 1: Завантаження базових зображень з мережі...";

            var cachedImages = new List<Bitmap>();
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            foreach (var url in baseUrls)
            {
                try
                {
                    var data = await client.GetByteArrayAsync(url);
                    using var ms = new MemoryStream(data);
                    using var temp = new Bitmap(ms);
                    cachedImages.Add(new Bitmap(temp)); 
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Помилка завантаження: {ex.Message}");
                }
            }

            if (cachedImages.Count == 0)
            {
                MessageBox.Show("Не вдалося завантажити картинки. Перевір підключення до інтернету.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                return;
            }

            progressBar.Value = 30;
            string selectedFilter = comboFilter.SelectedItem?.ToString() ?? "Інверсія кольорів";
            lblTime.Text = $"Крок 2: Обробка великого датасету фільтром '{selectedFilter}'...";

            int threads = (int)numericThreads.Value;
            IImageProcessor processor = rbParallel.Checked ?
                new ParallelProcessor() : new SequentialProcessor();

            var largeDataset = new List<Bitmap>();
            for (int i = 0; i < 10; i++)
            {
                foreach (var img in cachedImages)
                {
                    largeDataset.Add(new Bitmap(img));
                }
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            int counter = 0;
            foreach (var originalImg in largeDataset)
            {
                Bitmap processedImg = await Task.Run(() => processor.Process(originalImg, threads, selectedFilter));
                counter++;
                string imageName = $"Dataset_Image_{counter:D2}.png";
                _processedDataset.Add((imageName, originalImg, processedImg));

                listBoxUrls.Items.Add(imageName);
                int currentProgress = 30 + (int)((counter * 70.0) / largeDataset.Count);
                progressBar.Value = Math.Min(100, currentProgress);
            }

            watch.Stop();

            progressBar.Value = 100;
            btnStart.Enabled = true;
            lblTime.Text = $"[{processor.Name}] Фільтр: {selectedFilter} | Потоків: {threads} | Час: {watch.ElapsedMilliseconds} мс";

            if (listBoxUrls.Items.Count > 0)
            {
                listBoxUrls.SelectedIndex = 0;
            }
        }
        private void ListBoxUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBoxUrls.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < _processedDataset.Count)
            {
                var item = _processedDataset[selectedIndex];
                picOriginal.Image = item.Original;
                picProcessed.Image = item.Processed;
                picOriginal.Refresh();
                picProcessed.Refresh();
            }
        }
    }
}