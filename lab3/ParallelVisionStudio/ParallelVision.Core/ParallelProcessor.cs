using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ParallelVision.Core
{
    public class ParallelProcessor : IImageProcessor
    {
        public string Name => "Паралельний (TPL)";

        public Bitmap Process(Bitmap source, int threadsCount)
        {
            Bitmap bmp = (Bitmap)source.Clone();
            int width = bmp.Width;
            int height = bmp.Height;

            var bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            int bytes = Math.Abs(bmpData.Stride) * height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            var options = new ParallelOptions { MaxDegreeOfParallelism = threadsCount };

            // Розпаралелюємо важку обробку
            Parallel.For(0, bytes / 4, options, i =>
            {
                int idx = i * 4;

                // Симулюємо важкий алгоритм (наприклад, багаторазову фільтрацію)
                for (int iteration = 0; iteration < 50; iteration++)
                {
                    rgbValues[idx] = (byte)(255 - rgbValues[idx]);
                    rgbValues[idx + 1] = (byte)(255 - rgbValues[idx + 1]);
                    rgbValues[idx + 2] = (byte)(255 - rgbValues[idx + 2]);
                }
            });

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}
