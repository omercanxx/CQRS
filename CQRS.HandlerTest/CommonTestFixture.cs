using AutoMapper;
using CQRS.Application.AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.HandlerTest
{
    public class CommonTestFixture
    {
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<MapProfile>();
            }).CreateMapper();

        }
    }
}
