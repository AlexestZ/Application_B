using System.Collections.Concurrent;

namespace MyRabbitMQService.Models
{
    public class UserCache
    {
        public static ConcurrentBag<User> Cache { get; set; } = new ConcurrentBag<User>();
    }
}