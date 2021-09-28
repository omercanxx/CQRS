using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.OrderRequests
{
    public class OrderCreateRequest
    {
        [DisplayName("İsim")]
        [Required]
        public string Title { get; set; }
        [DisplayName("Fiyat")]
        [Required]
        public decimal Price { get; set; }
    }
}
