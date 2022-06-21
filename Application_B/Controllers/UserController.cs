using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyRabbitMQService.BL.Interfaces;

namespace MyRabbitMQService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase    
    {
        private readonly ILogger<UserController> _logger;
        private readonly IKafkaProducer _kafkaProducer;

        public UserController(ILogger<UserController> logger, IKafkaProducer kafkaProducer)
        {
            _logger = logger;
            _kafkaProducer = kafkaProducer;
        }
    }
}
