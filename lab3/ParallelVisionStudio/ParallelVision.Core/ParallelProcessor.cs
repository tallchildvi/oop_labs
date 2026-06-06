using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelVision.Core
{
    public class ParallelProcessor : IImageProcessor
    {
        public string Name => "Паралельний (TPL)";

        public byte[] Process(byte[] imageData, int threadsCount)
        {
            byte[] result = new byte[imageData.Length];
            var options = new ParallelOptions { MaxDegreeOfParallelism = threadsCount };

            Parallel.For(0, imageData.Length, options, i =>
            {
                result[i] = (byte)(255 - imageData[i]);
            });

            return result;
        }
    }
}
