using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommands
{
    public class UserCreateCommand : IRequest<CommandResult>
    {
        public UserCreateCommand(string name, string surname, string email, string password)
        {
            Name = name;
            Surname = surname;
            Password = password;
            Email = email;
        }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
