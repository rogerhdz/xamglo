using System;
using System.Collections.Generic;
using System.Web.Http;
using XamarinPO.Api.Models;

namespace XamarinPO.Api.Controllers
{
    public class TablesController : ApiController
    {
        [AcceptVerbs("Get")]
        public IEnumerable<Order> Orders()
        {
            var orders = new List<Order>();
            for (int i = 1; i <= 10; i++)
            {
                orders.Add(new Order
                {
                    Client = $"Client {i}",
                    Description = $"Description Order {i}",
                    DeliveryInformation = $"Delivery Information Order {i}",
                    Id = i,
                    CreationDate = DateTime.Now.AddDays(i),
                    DeliveryDate = DateTime.Now.AddDays(5 + i),
                    Title = $"Order's Title {i}"
                });
            }
            return orders;
        }

        [HttpGet]
        public IEnumerable<MenuItem> Menu([FromUri]string lang="en")
        {
            var menu = new List<MenuItem>();

            if (lang?.ToLower() == "es")
            {
                menu.Add(new MenuItem
                {
                    Icon = "ic_menu_orders",
                    Title = "Órdenes",
                    PageName = "NewOrder"
                });
                menu.Add(new MenuItem
                {
                    Icon = "ic_menu_client",
                    Title = "Clientes",
                    PageName = "ClientPage"
                });
                menu.Add(new MenuItem
                {
                    Icon = "ic_aboutus",
                    Title = "Acerca de",
                    PageName = "AboutUsPage"
                });
            }
            else
            {
                menu.Add(new MenuItem
                {
                    Icon = "ic_menu_orders",
                    Title = "Orders",
                    PageName = "NewOrder"
                });
                menu.Add(new MenuItem
                {
                    Icon = "ic_menu_client",
                    Title = "Clients",
                    PageName = "ClientPage"
                });
                menu.Add(new MenuItem
                {
                    Icon = "ic_aboutus",
                    Title = "About Us",
                    PageName = "AboutUsPage"
                });
            }
            return menu;
        }
    }
}
