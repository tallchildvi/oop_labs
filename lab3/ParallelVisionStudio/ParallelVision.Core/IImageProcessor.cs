using System.Drawing;

namespace ParallelVision.Core
{
    public interface IImageProcessor
    {
        string Name { get; }
        Bitmap Process(Bitmap source, int threadsCount, string filterType);
    }
}
