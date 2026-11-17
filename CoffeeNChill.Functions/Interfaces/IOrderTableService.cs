using CoffeeNChill.Functions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNChill.Functions.Interfaces
{
    public interface IOrderTableService
    {
        Task CreateOrderAsync(OrderEntity order);

        Task UpdateOrderStatusAsync(
            string partitionKey,
            string orderId,
            string status);
    }
}
