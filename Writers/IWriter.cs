using System.IO;

namespace Porpy.Writers
{
    public interface IWriter<TRequest>
    {
        void Write(StreamWriter writer, TRequest entity);
    }
}
