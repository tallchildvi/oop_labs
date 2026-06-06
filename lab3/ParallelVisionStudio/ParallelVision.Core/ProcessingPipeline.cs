using System;
using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace ParallelVision.Core
{
    public class ProcessingPipeline
    {
        private readonly BlockingCollection<byte[]> _downloadQueue = new();
        private readonly BlockingCollection<byte[]> _processedQueue = new();
        private readonly IImageProcessor _processor;
        private readonly int _threadsCount;

        public event Action<int> ProgressChanged;
        public BlockingCollection<byte[]> OutputQueue => _processedQueue;

        public ProcessingPipeline(IImageProcessor processor, int threadsCount)
        {
            _processor = processor;
            _threadsCount = threadsCount;
        }

        public async Task StartAsync(string[] urls)
        {
            var downloadTask = Task.Run(async () =>
            {
                using var client = new HttpClient();
                foreach (var url in urls)
                {
                    try
                    {
                        byte[] data = await client.GetByteArrayAsync(url);
                        _downloadQueue.Add(data);
                    }
                    catch { /* Обробка помилок завантаження */ }
                }
                _downloadQueue.CompleteAdding();
            });

            var processingTask = Task.Run(() =>
            {
                int completed = 0;
                var options = new ParallelOptions { MaxDegreeOfParallelism = _threadsCount };

                Parallel.ForEach(_downloadQueue.GetConsumingEnumerable(), options, item =>
                {
                    byte[] processed = _processor.Process(item, 1);
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
