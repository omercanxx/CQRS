using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq
{
    public class RabbitMqConfiguration
    {
        public string QueueName { get; set; }
        public string Hostname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
