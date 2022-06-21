using System.Threading.Tasks;

namespace MyRabbitMQService.BL.DataFlow
{
    public interface IUserDataFlow
    {
        Task SendUser(byte[] data);
    }
}