using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    //Commandların dönüş değerlerini özelleştirmek için CommandResult kullanılmıştır, farklı amaçlara hizmet etmesi amacıyla da birden fazla constructor kullanılmıştır.
    public class CommandResult
    {
        public CommandResult(Guid id)
        {
            Id = id;
        }
        public CommandResult(string name)
        {
            Name = name;
        }
        public CommandResult(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
    }
}
