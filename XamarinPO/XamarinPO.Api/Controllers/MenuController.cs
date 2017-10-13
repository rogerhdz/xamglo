using System.Collections.Generic;
using System.Web.Http;
using XamarinPO.Api.Models;

namespace XamarinPO.Api.Controllers
{
    public class MenuController : ApiController
    {
        public IEnumerable<MenuItem> GetMenu()
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
                Icon = "ic_aboutus",
                Title = "About Us",
                PageName = "AboutUsPage"
            });
            return menu;
        }
    }
}
