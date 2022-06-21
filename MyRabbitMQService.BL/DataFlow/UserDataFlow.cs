using MessagePack;
using MyRabbitMQService.BL.Interfaces;
using MyRabbitMQService.Models;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MyRabbitMQService.BL.DataFlow
{
    public class UserDataFlow : IUserDataFlow
    {
        private readonly TransformBlock<byte[], User> entryBlock;

        private readonly IKafkaProducer _kafkaProducer;

        public UserDataFlow(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;


            entryBlock = new TransformBlock<byte[], User>(data =>
              MessagePackSerializer.Deserialize<User>(data)
             );

            var enrichBlock = new TransformBlock<User, User>(u =>
            {
                u.Name = "edited";
                u.LastUpdated = DateTime.Now;
                return u;
            });

            var publishBlock = new ActionBlock<User>(u =>
            {
                Console.WriteLine($"Username {u.Name} ,LastUpdate:{u.LastUpdated}");

                _kafkaProducer.SendUserKafka(u);
            });

            var linkOptions = new DataflowLinkOptions()
            {
                PropagateCompletion = true
            };

            entryBlock.LinkTo(enrichBlock, linkOptions);
            enrichBlock.LinkTo(publishBlock, linkOptions);
        }
        public async Task SendUser(byte[] data)
        {
            await entryBlock.SendAsync(data);
        }
    }
}