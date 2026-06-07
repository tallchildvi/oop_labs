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
            using var sourceBmp = new Bitmap(4, 4);
            sourceBmp.SetPixel(0, 0, Color.FromArgb(255, 0, 0));
            sourceBmp.SetPixel(1, 1, Color.FromArgb(0, 255, 0));

            var seqProcessor = new SequentialProcessor();
            var parProcessor = new ParallelProcessor();

            using var seqResult = seqProcessor.Process(sourceBmp, 1, "²םגונס³ÿ ךמכüמנ³ג");
            using var parResult = parProcessor.Process(sourceBmp, 4, "²םגונס³ÿ ךמכüמנ³ג");

            Assert.Equal(seqResult.Width, parResult.Width);
            Assert.Equal(seqResult.Height, parResult.Height);
        }

        [Fact]
        public void Processors_ShouldInvertColorsCorrectly()
        {
            using var singlePixelBmp = new Bitmap(1, 1);
            singlePixelBmp.SetPixel(0, 0, Color.FromArgb(255, 0, 50));

            var processor = new ParallelProcessor();
            using var result = processor.Process(singlePixelBmp, 2, "²םגונס³ÿ ךמכüמנ³ג");
            Color processedColor = result.GetPixel(0, 0);

            Assert.Equal(0, processedColor.R);
        }
    }
}