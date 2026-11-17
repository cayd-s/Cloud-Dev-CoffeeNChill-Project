using CoffeeNChill.Functions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNChill.Functions.Interfaces
{
    public interface IOrderQueueService
    {
        Task SendOrderAsync(Order order);
    }
}
