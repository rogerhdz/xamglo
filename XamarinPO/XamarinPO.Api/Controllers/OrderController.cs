using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinPO.Api.Models;

namespace XamarinPO.Api.Controllers
{
    public class OrderController : ApiController
    {
        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            for (int i = 1; i <= 10; i++)
            {
                orders.Add(new Order()
                {
                    ClientId = Guid.NewGuid(),
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
