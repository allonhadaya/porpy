namespace Porpy.Serializers
{
    public interface ISerializer<TRequest>
    {
        byte[] Serialize(TRequest entity);
    }
}
