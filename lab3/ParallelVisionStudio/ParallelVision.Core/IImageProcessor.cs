namespace ParallelVision.Core
{
    public interface IImageProcessor
    {
        string Name { get; }
        byte[] Process(byte[] imageData, int threadsCount);
    }
}
