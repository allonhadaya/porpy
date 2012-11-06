using System.IO;

namespace Porpy.Readers
{
    public interface IReader<TResponse>
    {
        TResponse Read(StreamReader reader);
    }
}
