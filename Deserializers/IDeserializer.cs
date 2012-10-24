namespace Porpy.Deserializers
{
    public interface IDeserializer<TResponse>
    {
        TResponse Deserialize(byte[] entity);
    }
}
