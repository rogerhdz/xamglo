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
        #endregion

        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<OrderViewModel> Orders { get; set; }
        public NavigationService NavigationService { get; }

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
            ApplicationPropertiesManager.ClearAll();
            //GetApiUrl();
            LoadMenu();
            LoadOrders();
        }

        #region Methods
        void GetApiUrl()
        {
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
            var manager = new HttpManager<SettingsViewModel.Settings>();
            //Get object with items, isSuccess and message
            HttpManagerResult<SettingsViewModel.Settings> result = manager.HttpGetAzureList(config).Result;
            Result = result.Message;
            var settingsObj = ((List<SettingsViewModel.Settings>)result.ObjectResult)[0];
            if (!result.Success)
            {
                settingsObj.ServerUrl = "Error ocurred";
            }
            else
            {
                ApplicationPropertiesManager.Save("ApiServer", settingsObj.ServerUrl);
                LoadMenu();
                LoadOrders();
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
            IsRunning = true;
            Result = "Loading Results";
            //Create configurator to know consume api
            var config = new HttpManagerConfiguration
            {
                Method = "api/tables/orders",
                Server = "http://xamarinpoapi.azurewebsites.net"
            };
            //Create manager
            var manager = new HttpManager<OrderViewModel>();
            //Get object with items, isSuccess and message
            HttpManagerResult<OrderViewModel> result = await manager.HttpGetList(config);
            Result = result.Message;
            if (result.Success)
            {
                Orders.Clear();
                Orders.AddRange((List<OrderViewModel>)result.ObjectResult);
            }

            IsRunning = false;
        }

        async Task LoadMenu()
        {
            //Instance result observable list
            Menu = new ObservableCollection<MenuItemViewModel>();
            IsRunning = true;
            Result = "Loading Menu";

            //Create configurator to know consume api
            var config = new HttpManagerConfiguration
            {
                Method = "api/menu/getmenu",
                Server = "http://xamarinpoapi.azurewebsites.net"
            };
            //Create manager
            var manager = new HttpManager<MenuItemViewModel>();
            //Get object with items, isSuccess and message
            HttpManagerResult<MenuItemViewModel> result = await manager.HttpGetList(config);
            Result = result.Message;
            if (result.Success)
            {
                Menu.Clear();
                Menu.AddRange((List<MenuItemViewModel>)result.ObjectResult);
            }
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
