using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinPO.Services;
using XamarinPO.ViewModel.Menu;
using XamarinPO.ViewModel.Order;
using XamarinPO.Views.Order;

namespace XamarinPO.ViewModel
{
    public class MainViewModel
    {
        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<OrderViewModel> Orders { get; set; }
        public NavigationService NavigationService { get; set; }
        #endregion
        public MainViewModel()
        {
            NavigationService = new NavigationService();
            LoadMenu();
            LoadOrders();
        }

        #region Methods
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
                PageName = "MainPage"
            });
            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ic_menu_client",
                Title = "Clients",
                PageName = "ClientPage"
            });
        }
        #endregion

        #region Commands
        public ICommand GoToCommand {
            get
            {
                return new RelayCommand<string>(GoTo);
            }
                
        }

        private void GoTo(string PageName)
        {
            NavigationService.Navigate(PageName);
        }


        #endregion
    }
}
