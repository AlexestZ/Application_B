using MessagePack;
using MyRabbitMQService.BL.Interfaces;
using MyRabbitMQService.Models;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace MyRabbitMQService.BL.Services
{
    public class RabbitMQService : IRabbitMqService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        public RabbitMQService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("testUser", ExchangeType.Fanout);

            _channel.QueueDeclare("testUser", true, false, autoDelete: false);
        }
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
        public async Task GetUserAsync(User u)
        {
            await Task.Factory.StartNew(() =>
            {
                var body = MessagePackSerializer.Serialize(u);

                _channel.BasicPublish("", "testUser", body: body);
            });
        }
    }
}