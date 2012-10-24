using System.IO;

namespace Porpy.Serializers
{
    public interface ISerializer<TRequest>
    {
        void Serialize(StreamWriter writer, TRequest entity);
    }
}
