using System.Drawing;
using ParallelVision.Core;
using Xunit;

namespace ParallelVision.Tests
{
    public class ProcessorTests
    {
        [Fact]
        public void Processors_ShouldReturnIdenticalPixelResults()
        {
            // Arrange: Створюємо тестове зображення 4x4 пікселі
            using var sourceBmp = new Bitmap(4, 4);

            // Заповнюємо кілька точок тестовими кольорами
            sourceBmp.SetPixel(0, 0, Color.FromArgb(255, 0, 0));     // Яскраво-червоний
            sourceBmp.SetPixel(1, 1, Color.FromArgb(0, 255, 0));     // Яскраво-зелений
            sourceBmp.SetPixel(2, 2, Color.FromArgb(0, 0, 255));     // Яскраво-синій
            sourceBmp.SetPixel(3, 3, Color.FromArgb(128, 128, 128)); // Сірий

            var seqProcessor = new SequentialProcessor();
            var parProcessor = new ParallelProcessor();

            // Act: Обробляємо послідовно (1 потік) та паралельно (4 потоки)
            using var seqResult = seqProcessor.Process(sourceBmp, 1);
            using var parResult = parProcessor.Process(sourceBmp, 4);

            // Assert: Перевіряємо, чи збігаються геометричні розміри
            Assert.Equal(seqResult.Width, parResult.Width);
            Assert.Equal(seqResult.Height, parResult.Height);

            // Попіксельно порівнюємо масиви кольорів після обробки
            for (int x = 0; x < seqResult.Width; x++)
            {
                for (int y = 0; y < seqResult.Height; y++)
                {
                    Color seqColor = seqResult.GetPixel(x, y);
                    Color parColor = parResult.GetPixel(x, y);

                    // Кольори мають бути абсолютно ідентичними
                    Assert.Equal(seqColor.R, parColor.R);
                    Assert.Equal(seqColor.G, parColor.G);
                    Assert.Equal(seqColor.B, parColor.B);
                }
            }
        }

        [Fact]
        public void Processors_ShouldInvertColorsCorrectly()
        {
            // Arrange: Перевіримо математику інверсії на одному пікселі
            using var singlePixelBmp = new Bitmap(1, 1);
            singlePixelBmp.SetPixel(0, 0, Color.FromArgb(255, 0, 50)); // R=255, G=0, B=50

            var processor = new ParallelProcessor();

            // Act
            using var result = processor.Process(singlePixelBmp, 2);
            Color processedColor = result.GetPixel(0, 0);

            // Assert: Очікуємо повну інверсію (255 - оригінал)
            Assert.Equal(0, processedColor.R);   // 255 - 255
            Assert.Equal(255, processedColor.G); // 255 - 0
            Assert.Equal(205, processedColor.B); // 255 - 50
        }
    }
}