using MessagePack;
using Microsoft.Extensions.Hosting;
using MyRabbitMQService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyRabbitMQService.BL.Services
{
    public class MyRabbitMQConsumer : IHostedService, IDisposable
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public MyRabbitMQConsumer()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("person", ExchangeType.Fanout, durable: false);

            _channel.QueueDeclare("person", true, false, autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, ea) =>
            {
                User user = MessagePackSerializer.Deserialize<User>(ea.Body);

                Console.WriteLine($"id {user.Id} - Name {user.Name}");

                UserCache.Cache.Add(user);
            };
            _channel.BasicConsume("user", true, consumer);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}