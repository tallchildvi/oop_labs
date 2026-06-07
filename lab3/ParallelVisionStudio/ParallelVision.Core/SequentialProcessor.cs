using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ParallelVision.Core
{
    public class SequentialProcessor : IImageProcessor
    {
        public string Name => "Послідовний";

        public Bitmap Process(Bitmap source, int threadsCount, string filterType)
        {
            Bitmap bmp = (Bitmap)source.Clone();
            int width = bmp.Width;
            int height = bmp.Height;

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            int bytes = Math.Abs(bmpData.Stride) * height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            for (int i = 0; i < bytes / 4; i++)
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
                    rgbValues[idx] = gray;
                    rgbValues[idx + 1] = gray;
                    rgbValues[idx + 2] = gray;
                }
                else if (filterType == "Штучне засвітлення")
                {
                    rgbValues[idx] = (byte)Math.Min(255, b + 40);
                    rgbValues[idx + 1] = (byte)Math.Min(255, g + 40);
                    rgbValues[idx + 2] = (byte)Math.Min(255, r + 40);
                }
                else 
                {
                    rgbValues[idx] = (byte)(255 - b);
                    rgbValues[idx + 1] = (byte)(255 - g);
                    rgbValues[idx + 2] = (byte)(255 - r);
                }
            }

            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}