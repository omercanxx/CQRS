﻿using AutoMapper;
using CQRS.Core;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Domain.MongoDtos.Orders;
using CQRS.Infrastructure;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.OrderCommands
{
    public class OrderCommandHandler : IRequestHandler<OrderCreateCommand, CommandResult>,
                                       IRequestHandler<OrderUpdateCommand, CommandResult>,
                                       IRequestHandler<OrderDeleteCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ICqrsDatabaseSettings _settings;
        private IMongoCollection<CreatedOrderDto> _orders;
        public OrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ICqrsDatabaseSettings settings)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _settings = settings;
        }
        public async Task<CommandResult> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            Order order = new Order(command.UserId, command.CourseId, command.Price);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            
            var mongoDatabase = getMongoDatabase(_settings);
            _orders = mongoDatabase.GetCollection<CreatedOrderDto>("orders");
            _orders.InsertOne(_mapper.Map<CreatedOrderDto>(order));

            //CommandResultun Id propertysine set edilen constructor ın içerisine girecektir.
            return new CommandResult(order.Id);
        }

        public async Task<CommandResult> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            dbOrder.UpdateUser(command.UserId);
            dbOrder.UpdateCourse(command.CourseId);
            dbOrder.UpdatePrice(command.Price);


            _orderRepository.Update(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(dbOrder.Id);
        }

        public async Task<CommandResult> Handle(OrderDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbOrder = await _orderRepository.GetByIdAsync(command.Id);

            _orderRepository.Deactivate(dbOrder);
            await _orderRepository.SaveChangesAsync();

            return new CommandResult(dbOrder.Id);
        }
        private IMongoDatabase getMongoDatabase(ICqrsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            return database;
        }
    }
}
