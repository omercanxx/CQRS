using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq
{
    public interface IMessage
    {
        string Text { get; set; }
    }
}
