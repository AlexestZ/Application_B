using MessagePack;
using System;

namespace MyRabbitMQService.Models
{
    [MessagePackObject]
    public class User
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Name { get; set; }

        [Key(2)]
        public DateTime LastUpdated { get; set; }
    }
}