using CQRS.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.UserCommand
{
    public class UserUpdateCommand : IRequest<CommandResult>
    {
        public UserUpdateCommand(Guid id, string name, string surname, string email, string password, DateTime birthdate)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Password = password;
            Email = email;
            Birthdate = birthdate;
        }
        public Guid Id{ get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname => $"{Name} {Surname}";
        public DateTime Birthdate { get; set; }
    }
}
