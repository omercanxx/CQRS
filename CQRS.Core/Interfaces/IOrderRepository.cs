﻿using CQRS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Interfaces
{
    public interface IOrderRepository : ICustomRepository<Order>
    {
    }
}
