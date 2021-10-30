using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
        }

        public User(string name, string surname, string email, string userName)
        {
            Name = name;
            Surname = surname;
            Email = email;
            UserName = userName;
            IsActive = true;
            CreatedOn = DateTime.Now;
        }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string FullName => $"{Name} {FullName}";
        public bool IsActive { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public ICollection<Order> Orders { get; protected set; }
    }
}