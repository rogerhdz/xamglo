using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PCLAppConfig;
using XamarinPO.Extensions;
using XamarinPO.Services;
using XamarinPO.Helpers;
using XamarinPO.ViewModel.Application;
using XamarinPO.ViewModel.Menu;
using XamarinPO.ViewModel.Order;
using Xamarin.Forms;

namespace XamarinPO.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        bool _isRunning;
        string _result;
        private ObservableCollection<OrderViewModel> _orders;
        private ObservableCollection<MenuItemViewModel> _menu;
        #endregion

        #region Properties

        public NavigationService NavigationService { get; }

        public ObservableCollection<MenuItemViewModel> Menu
        {
            get => _menu ?? (_menu = new ObservableCollection<MenuItemViewModel>());
            set
            {
                if (_menu == Menu) return;
                _menu = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Menu)));
            }
        }
        public ObservableCollection<OrderViewModel> Orders
        {
            get => _orders ?? (_orders = new ObservableCollection<OrderViewModel>());
            set
            {
                if (_orders == Orders) return;
                _orders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Orders)));
            }
        }
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning == IsRunning) return;
                _isRunning = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
            }
        }
        public string Result
        {
            get => _result;
            set
            {
                if (_result == Result) return;
                _result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }
        #endregion 

        public MainViewModel()
        {
            NavigationService = new NavigationService();
        }

        #region Methods

        public async Task InitApp()
        {
            ApplicationPropertiesManager.ClearAll();
            await GetApiUrl();
            await LoadMenu();
            await LoadOrders();
        }

        async Task GetApiUrl()
        {
            Settings settingsObj = new Settings();
            //Instance result observable list
            IsRunning = true;
            Result = "Loading Settings";
            //Create configurator to consume api
            var config = new HttpManagerConfiguration
            {
                Method = "/Tables/Settings",
                Server = ConfigurationManager.AppSettings["SettingsServer"]
            };

            //Create manager
            var manager = new HttpManager<Settings>();
            //Get object with items, isSuccess and message
            HttpManagerResult result = await manager.HttpGetAzureList(config);
            Result = result.Message;

            if (result.Success)
            {
                settingsObj = ((List<Settings>) result.ObjectResult)[0];
                await ApplicationPropertiesManager.Save("ApiServer", settingsObj);
            }
            else
            {
                settingsObj.ServerUrl = "Error ocurred";
            }
            IsRunning = false;
        }

        /// <summary>
        /// Loads the orders from API
        /// </summary>
        /// <returns></returns>
        async Task LoadOrders()
        {
            
            //Instance result observable list
            Orders = new ObservableCollection<OrderViewModel>();
            Orders.Clear();
            IsRunning = true;
            Result = "Loading Results";
            //Create configurator to know consume api
            var config = new HttpManagerConfiguration
            {
                Method = "api/tables/orders",
                Server = ApplicationPropertiesManager.Load<Settings>("ApiServer").ServerUrl
            };
            //Create manager
            var manager = new HttpManager<OrderViewModel>();
            //Get object with items, isSuccess and message
            HttpManagerResult result = await manager.HttpGetList(config);
            Result = result.Message;
            if (result.Success)
            {
                Orders.AddRange((List<OrderViewModel>)result.ObjectResult);
            }

            IsRunning = false;
        }

        async Task LoadMenu()
        {
            //Instance result observable list
            Menu = new ObservableCollection<MenuItemViewModel>();
            Menu.Clear();
            IsRunning = true;
            Result = "Loading Menu";

            //Create configurator to know consume api
            var config = new HttpManagerConfiguration
            {
                Method = "api/tables/menu",
                Server = ApplicationPropertiesManager.Load<Settings>("ApiServer").ServerUrl
            };
            //Create manager
            var manager = new HttpManager<MenuItemViewModel>();
            //Get object with items, isSuccess and message
            HttpManagerResult result = await manager.HttpGetList(config);
            Result = result.Message;
            if (result.Success)
            {
                Menu.AddRange((List<MenuItemViewModel>)result.ObjectResult);
            }
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_menu_settings",
                Title = "Settings",
                PageName = "SettingsPage"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_menu_testapi",
                Title = "Test Api",
                PageName = "TestApiPage"
            });
            IsRunning = false;
        }
        #endregion

        #region Commands
        public ICommand GoToCommand
        {
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
