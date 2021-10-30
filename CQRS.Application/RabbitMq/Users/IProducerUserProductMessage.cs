using CQRS.Core.Entities.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.RabbitMq.Users
{
    public interface IProducerUserProductMessage
    {
        void SendUserProductMessage(MongoUserProduct userProduct);
    }
}
