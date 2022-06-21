using System.Threading;
using System.Threading.Tasks;

namespace MyRabbitMQService.BL.Services
{
    internal class MyRabbitMQConsumerBase
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}