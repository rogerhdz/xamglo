using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinPO.ViewModel.Menu;
using XamarinPO.ViewModel.Order;

namespace XamarinPO.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<OrderViewModel> Orders { get; set; }
        public MainViewModel()
        {
            LoadMenu();
            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders = new ObservableCollection<OrderViewModel>();
            for (int i = 1; i <= 10; i++)
            {
                Orders.Add(new OrderViewModel()
                {
                    Client = i,
                    Description = string.Format("Description Order {0}", i),
                    DeliveryInformation = string.Format("Delivery Information Order {0}", i),
                    Id = 1,
                    CreationDate = DateTime.Now.AddDays(i),
                    DeliveryDate = DateTime.Now.AddDays(5 + i),
                    Title = string.Format("Order's Title {0}", i)
                });
            }
        }

        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_menu_orders",
                Title = "Orders",
                PageName = "NewOrder"
            });
            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_menu_client",
                Title = "Clients",
                PageName = "Clients"
            });
        }
    }
}
