using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.CourseDtos
{
    public class CourseDto
    {
        //Serialize işleminde propertyler için  Property name belirleniyor.
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        //Yeni eklenen kurs testi için eklenmiştir.
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }
    }
}
