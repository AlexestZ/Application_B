using MyRabbitMQService.Models;
using System.Threading.Tasks;

namespace MyRabbitMQService.BL.Interfaces
{
    public interface IKafkaProducer
    {
        Task SendUserKafka(User u);
    }
}