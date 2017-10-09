using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [AcceptVerbs("Get")]
        public IEnumerable<MenuItem> Menu()
        {
            var menu = new List<MenuItem>();
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
                Icon = "ic_menu_settings",
                Title = "Settings",
                PageName = "SettingsPage"
            });
            menu.Add(new MenuItem
            {
                Icon = null,
                Title = "About Us",
                PageName = "AboutUsPage"
            });
            menu.Add(new MenuItem
            {
                Icon = null,
                Title = "Close",
                PageName = "CloseApp"
            });
            return menu;
        }
    }
}
