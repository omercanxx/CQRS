using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class OrderController : IClassFixture<ApiFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        public OrderController(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }
    }
}
