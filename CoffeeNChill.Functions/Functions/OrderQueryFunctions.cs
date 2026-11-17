using CoffeeNChill.Functions.Interfaces;
using CoffeeNChill.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace CoffeeNChill.Functions.Functions
{
    public class OrderQueryFunctions
    {
        private readonly IOrderTableService _orderTableService;
        private readonly ILogger<OrderQueryFunctions> _logger;

        public OrderQueryFunctions(
            IOrderTableService orderTableService,
            ILogger<OrderQueryFunctions> logger)
        {
            _orderTableService = orderTableService;
            _logger = logger;
        }

        [Function("GetAllOrders")]
        public async Task<HttpResponseData> GetAllOrders(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                "get",
                Route = "orders")]
            HttpRequestData req)
        {
            try
            {
                var orders =
                    await _orderTableService.GetAllOrdersAsync();

                HttpResponseData response =
                    req.CreateResponse(HttpStatusCode.OK);

                await response.WriteAsJsonAsync(orders);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to retrieve all orders.");

                HttpResponseData response =
                    req.CreateResponse(
                        HttpStatusCode.InternalServerError);

                await response.WriteAsJsonAsync(new
                {
                    error = "Unable to retrieve orders."
                });

                return response;
            }
        }

        [Function("GetOrder")]
        public async Task<HttpResponseData> GetOrder(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                "get",
                Route = "orders/{orderDate}/{orderId}")]
            HttpRequestData req,
            string orderDate,
            string orderId)
        {
            bool validDate = DateTime.TryParseExact(
                orderDate,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);

            if (!validDate)
            {
                HttpResponseData badRequest =
                    req.CreateResponse(
                        HttpStatusCode.BadRequest);

                await badRequest.WriteAsJsonAsync(new
                {
                    error = "orderDate must use yyyy-MM-dd format."
                });

                return badRequest;
            }

            if (string.IsNullOrWhiteSpace(orderId))
            {
                HttpResponseData badRequest =
                    req.CreateResponse(
                        HttpStatusCode.BadRequest);

                await badRequest.WriteAsJsonAsync(new
                {
                    error = "Order ID is required."
                });

                return badRequest;
            }

            try
            {
                OrderEntity? order =
                    await _orderTableService.GetOrderAsync(
                        orderDate,
                        orderId);

                if (order == null)
                {
                    HttpResponseData notFound =
                        req.CreateResponse(
                            HttpStatusCode.NotFound);

                    await notFound.WriteAsJsonAsync(new
                    {
                        error = "Order not found."
                    });

                    return notFound;
                }

                HttpResponseData response =
                    req.CreateResponse(HttpStatusCode.OK);

                await response.WriteAsJsonAsync(order);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to retrieve order {OrderId}.",
                    orderId);

                HttpResponseData response =
                    req.CreateResponse(
                        HttpStatusCode.InternalServerError);

                await response.WriteAsJsonAsync(new
                {
                    error = "Unable to retrieve the order."
                });

                return response;
            }
        }
    }
}

