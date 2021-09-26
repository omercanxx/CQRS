﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Requests.CourseRequests
{
    public class CourseCreateRequest
    {
        [DisplayName("İsim")]
        [Required]
        public string Title { get; set; }
        [DisplayName("Fiyat")]
        [Required]
        public decimal Price { get; set; }
    }
}