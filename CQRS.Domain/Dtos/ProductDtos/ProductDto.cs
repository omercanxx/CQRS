using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CQRS.Domain.Dtos.ProductDtos
{
    public class ProductDto
    {
        public ProductDto(Guid id, string title, decimal price)
        {
            Id = id;
            Title = title;
            Price = price;
            CreatedOn = DateTime.Now;
        }
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
        //Yeni eklenen kurs testi için eklenmiştir.
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        
    }
}
