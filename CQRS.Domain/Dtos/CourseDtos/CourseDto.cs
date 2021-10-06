using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.CourseDtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        //Yeni eklenen kurs testi için eklenmiştir.
        public DateTime CreatedOn { get; set; }
    }
}
