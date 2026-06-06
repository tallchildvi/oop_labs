using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelVision.Core
{
    public class SequentialProcessor : IImageProcessor
    {
        public string Name => "Послідовний";

        public byte[] Process(byte[] imageData, int threadsCount)
        {
            byte[] result = new byte[imageData.Length];
            for (int i = 0; i < imageData.Length; i++)
            {
                result[i] = (byte)(255 - imageData[i]);
            }
            return result;
        }
    }
}
