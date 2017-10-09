using System;
using System.Collections.Generic;
using System.Web.Http;
using XamarinPO.Api.Models;

namespace XamarinPO.Api.Controllers
{
    public class OrdersController : ApiController
    {
        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            for (int i = 1; i <= 10; i++)
            {
                orders.Add(new Order
                {
                    Client = string.Format("Client {0}", i),
                    Description = string.Format("Description Order {0}", i),
                    DeliveryInformation = string.Format("Delivery Information Order {0}", i),
                    Id = i,
                    CreationDate = DateTime.Now.AddDays(i),
                    DeliveryDate = DateTime.Now.AddDays(5 + i),
                    Title = string.Format("Order's Title {0}", i)
                });
            }
            return orders;
        }
    }
}
