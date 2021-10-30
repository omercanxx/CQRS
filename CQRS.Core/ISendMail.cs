using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    public interface ISendMail
    {
        void Send(string email, string subject, string body);
    }
}
