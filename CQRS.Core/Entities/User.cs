using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string surname, string email ,string password, DateTime birthdate)
        {
            Name = name;
            Surname = surname;
            Password = password;
            Email = email;
            Birthdate = birthdate;
        }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Fullname => $"{Name} {Surname}";
        public DateTime Birthdate { get; protected set; }
        public virtual ICollection<Order> Orders { get; protected set; }
        public void UpdateEmail(string email)
        {
            Email = email;
        }
        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateSurname(string surname)
        {
            Surname = surname;
        }
        public void UpdatePassword(string password)
        {
            Password = password;
        }
    }
}
