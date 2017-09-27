﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinPO.Api.Models;

namespace XamarinPO.Api.Controllers
{
    public class MenuController : ApiController
    {
        public IEnumerable<MenuItem> GetOrders()
        {
            var menu = new List<MenuItem>();
            menu.Add(new MenuItem()
            {
                Icon = "ic_menu_orders",
                Title = "Orders",
                PageName = "NewOrder"
            });
            menu.Add(new MenuItem()
            {
                Icon = "ic_menu_client",
                Title = "Clients",
                PageName = "Clients"
            });
            return menu;
        }
    }
}
