using Confluent.Kafka;
using MessagePack;
using System;

namespace MyRabbitMQService.BL.Common
{
    public class MsgPackDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) => MessagePackSerializer.Deserialize<T>(data.ToArray());
    }
}