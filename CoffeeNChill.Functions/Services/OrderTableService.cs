using Azure;
using Azure.Data.Tables;
using CoffeeNChill.Functions.Interfaces;
using CoffeeNChill.Functions.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeNChill.Functions.Services
{
    public class OrderTableService : IOrderTableService
    {
        private const string TableName = "Orders";

        private readonly TableClient _tableClient;
        private readonly ILogger<OrderTableService> _logger;

        public OrderTableService(
            IConfiguration configuration,
            ILogger<OrderTableService> logger)
        {
            _logger = logger;

            string connectionString =
                configuration["AzureWebJobsStorage"]
                ?? throw new InvalidOperationException(
                    "AzureWebJobsStorage connection string is not configured.");

            _tableClient = new TableClient(
                connectionString,
                TableName);
        }

        public async Task CreateOrderAsync(OrderEntity order)
        {
            try
            {
                await _tableClient.CreateIfNotExistsAsync();

                await _tableClient.AddEntityAsync(order);

                _logger.LogInformation(
                    "Order {OrderId} created in Orders table with status {Status}.",
                    order.OrderId,
                    order.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to create order {OrderId} in Orders table.",
                    order.OrderId);

                throw;
            }
        }

        public async Task UpdateOrderStatusAsync(
            string partitionKey,
            string orderId,
            string status)
        {
            try
            {
                Response<OrderEntity> response =
                    await _tableClient.GetEntityAsync<OrderEntity>(
                        partitionKey,
                        orderId);

                OrderEntity entity = response.Value;

                entity.Status = status;
                entity.LastUpdated = DateTimeOffset.UtcNow;

                await _tableClient.UpdateEntityAsync(
                    entity,
                    entity.ETag,
                    TableUpdateMode.Replace);

                _logger.LogInformation(
                    "Order {OrderId} status updated to {Status}.",
                    orderId,
                    status);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to update order {OrderId} to status {Status}.",
                    orderId,
                    status);

                throw;
            }
        }

        public async Task<List<OrderEntity>> GetAllOrdersAsync()
        {
            try
            {
                List<OrderEntity> orders = new List<OrderEntity>();

                await foreach (OrderEntity order in
                    _tableClient.QueryAsync<OrderEntity>())
                {
                    orders.Add(order);
                }

                _logger.LogInformation(
                    "Retrieved {OrderCount} orders from Orders table.",
                    orders.Count);

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to retrieve orders from Orders table.");

                throw;
            }
        }

        public async Task<OrderEntity?> GetOrderAsync(
            string partitionKey,
            string orderId)
        {
            try
            {
                Response<OrderEntity> response =
                    await _tableClient.GetEntityAsync<OrderEntity>(
                        partitionKey,
                        orderId);

                return response.Value;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                _logger.LogInformation(
                    "Order {OrderId} was not found.",
                    orderId);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to retrieve order {OrderId}.",
                    orderId);

                throw;
            }
        }
    }
}
