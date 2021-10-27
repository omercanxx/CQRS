using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq
{
    public class Message : IMessage
    {
        public Message(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
    }
}
