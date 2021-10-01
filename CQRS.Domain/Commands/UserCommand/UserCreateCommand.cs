using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommand
{
    public class UserCreateCommand : IRequest<CommandResult>
    {
        public UserCreateCommand(string name, string surname, string email, string password, DateTime birthdate)
        {
            Name = name;
            Surname = surname;
            Password = password;
            Email = email;
            Birthdate = birthdate;
        }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname => $"{Name} {Surname}";
        public DateTime Birthdate { get; set; }
    }
}
