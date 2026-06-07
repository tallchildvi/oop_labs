using Xunit;
using ParallelVision.Core;

namespace ParallelVision.Tests
{
    public class ProcessorTests
    {
        [Fact]
        public void Processors_ShouldReturnIdenticalResults()
        {
            // Arrange
            byte[] input = { 0, 50, 100, 150, 200, 255 };
            var seq = new SequentialProcessor();
            var par = new ParallelProcessor();

            // Act
            byte[] seqResult = seq.Process(input, 1);
            byte[] parResult = par.Process(input, 4);

            // Assert
            Assert.Equal(seqResult, parResult);
        }
    }
}