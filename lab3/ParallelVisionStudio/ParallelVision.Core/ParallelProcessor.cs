using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ParallelVision.Core
{
    public class ParallelProcessor : IImageProcessor
    {
        public string Name => "Паралельний (TPL)";

        public Bitmap Process(Bitmap source, int threadsCount, string filterType)
        {
            Bitmap bmp = (Bitmap)source.Clone();
            int width = bmp.Width;
            int height = bmp.Height;

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            int bytes = Math.Abs(bmpData.Stride) * height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);
            var options = new ParallelOptions { MaxDegreeOfParallelism = threadsCount };

            Parallel.For(0, bytes / 4, options, i =>
            {
                int idx = i * 4;

                byte b = rgbValues[idx];
                byte g = rgbValues[idx + 1];
                byte r = rgbValues[idx + 2];

                double dummy = 0;
                for (int iteration = 0; iteration < 150; iteration++)
                {
                    dummy += Math.Sin(b) * Math.Cos(g);
                }

                if (filterType == "Чорно-білий (Grayscale)")
                {
                    byte gray = (byte)(0.299f * r + 0.587f * g + 0.114f * b);
                    rgbValues[idx] = gray;     // B
                    rgbValues[idx + 1] = gray; // G
                    rgbValues[idx + 2] = gray; // R
                }
                else if (filterType == "Штучне засвітлення")
                {
                    rgbValues[idx] = (byte)Math.Min(255, b + 40);     // B
                    rgbValues[idx + 1] = (byte)Math.Min(255, g + 40); // G
                    rgbValues[idx + 2] = (byte)Math.Min(255, r + 40); // R
                }
                else
                {
                    rgbValues[idx] = (byte)(255 - b);     // B
                    rgbValues[idx + 1] = (byte)(255 - g); // G
                    rgbValues[idx + 2] = (byte)(255 - r); // R
                }
            });

            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}