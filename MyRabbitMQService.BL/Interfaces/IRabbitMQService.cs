using MyRabbitMQService.Models;
using System.Threading.Tasks;

namespace MyRabbitMQService.BL.Interfaces
{
    public interface IRabbitMqService
    {
        Task GetUserAsync(User u);
    }
}