using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ParallelVision.Core
{
    public class SequentialProcessor : IImageProcessor
    {
        public string Name => "Послідовний";

        public Bitmap Process(Bitmap source, int threadsCount)
        {
            Bitmap bmp = (Bitmap)source.Clone();
            int width = bmp.Width;
            int height = bmp.Height;

            // Блокуємо пікселі в пам'яті (формат 32 біти: ARGB)
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            int bytes = Math.Abs(bmpData.Stride) * height;
            byte[] rgbValues = new byte[bytes];

            // Копіюємо в масив
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            // Послідовно інвертуємо кольори (крок 4 байти: Blue, Green, Red, Alpha)
            for (int i = 0; i < bytes / 4; i++)
            {
                int idx = i * 4;
                rgbValues[idx] = (byte)(255 - rgbValues[idx]);     // B
                rgbValues[idx + 1] = (byte)(255 - rgbValues[idx + 1]); // G
                rgbValues[idx + 2] = (byte)(255 - rgbValues[idx + 2]); // R
            }

            // Записуємо назад у картинку
            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}
