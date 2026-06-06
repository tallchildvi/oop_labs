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
            string[] urls = { "https://picsum.photos/200/300", "https://picsum.photos/300/300", "https://picsum.photos/400/300" };
            int threads = (int)numericThreads.Value;

            var processor = new ParallelProcessor();
            var pipeline = new ProcessingPipeline(processor, threads);

            var progress = new Progress<int>(value => progressBar.Value = value);
            pipeline.ProgressChanged += value => ((IProgress<int>)progress).Report(value);

            btnStart.Enabled = false;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await pipeline.StartAsync(urls);

            watch.Stop();
            btnStart.Enabled = true;
            lblTime.Text = $"Завершено за: {watch.ElapsedMilliseconds} мс";

        }

    }
}
