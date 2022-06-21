using Confluent.Kafka;
using MessagePack;

namespace MyRabbitMQService.BL.Common
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context) => MessagePackSerializer.Serialize(data);
    }
}
