using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
