using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParallelVision.Core
{
    public class ProcessingPipeline
    {
        private readonly BlockingCollection<Bitmap> _downloadQueue = new();
        private readonly BlockingCollection<Bitmap> _processedQueue = new();
        private readonly IImageProcessor _processor;
        private readonly int _threadsCount;

        public event Action<int> ProgressChanged;
        public BlockingCollection<Bitmap> OutputQueue => _processedQueue;

        public ProcessingPipeline(IImageProcessor processor, int threadsCount)
        {
            _processor = processor;
            _threadsCount = threadsCount;
        }

        public async Task StartAsync(string[] urls, string filterType)
        {
            var downloadTask = Task.Run(async () =>
            {
                using var client = new HttpClient();
                foreach (var url in urls)
                {
                    try
                    {
                        byte[] data = await client.GetByteArrayAsync(url);
                        using var ms = new MemoryStream(data);
                        using var tempBmp = new Bitmap(ms);
                        Bitmap bmp = new Bitmap(tempBmp);
                        _downloadQueue.Add(bmp);
                    }
                    catch { }
                }
                _downloadQueue.CompleteAdding();
            });

            var processingTask = Task.Run(() =>
            {
                int completed = 0;
                var options = new ParallelOptions { MaxDegreeOfParallelism = _threadsCount };

                Parallel.ForEach(_downloadQueue.GetConsumingEnumerable(), options, bmp =>
                {
                    Bitmap processed = _processor.Process(bmp, 1, filterType);
                    _processedQueue.Add(processed);

                    completed++;
                    ProgressChanged?.Invoke((completed * 100) / urls.Length);
                });

                _processedQueue.CompleteAdding();
            });

            await Task.WhenAll(downloadTask, processingTask);
        }
    }
}