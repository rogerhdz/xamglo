using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinPO.Services;
using XamarinPO.Extensions;
using XamarinPO.Helpers;
using XamarinPO.ViewModel.Menu;
using XamarinPO.ViewModel.Order;
using XamarinPO.Views.Order;
using System.Collections.Generic;
using System.Linq;

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
        public NavigationService NavigationService { get; set; }
        public HttpManagerConfiguration ManagerConfiguration { get; set; }

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
            ManagerConfiguration = new HttpManagerConfiguration()
            {
                Server = "http://192.168.56.1:5555"
            };
            LoadMenu();
            LoadOrders();
        }
        #region Methods
        /// <summary>
        /// Loads the orders from API
        /// </summary>
        /// <returns></returns>
        async Task LoadOrders()
        {
            Orders = new ObservableCollection<OrderViewModel>();
            IsRunning = true;
            try
            {
                //Create configurator to know consume api
                ManagerConfiguration.Method = "/Api/Order/GetOrders";
                //Create manager
                var manager = new HttpManager<OrderViewModel>();
                //Get object with items, isSuccess and message
                HttpManagerResult<OrderViewModel> result = await manager.HttpGetList(ManagerConfiguration);
                Result = result.Message;
                if (result.Success)
                {
                    //Modify observable
                    Orders.AddRange(result.Items);
                }
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
            finally
            {
                IsRunning = false;
            }
        }

        async void LoadMenu()
        {
            //Instance result observable list
            Menu = new ObservableCollection<MenuItemViewModel>();
            IsRunning = true;

            try
            {
                //Create configurator to know consume api
                ManagerConfiguration.Method = "/Api/Menu/GetMenu";
                //Create manager
                var manager = new HttpManager<MenuItemViewModel>();
                //Get object with items, isSuccess and message
                HttpManagerResult<MenuItemViewModel> result = await manager.HttpGetList(ManagerConfiguration);
                Result = result.Message;
                if (result.Success)
                {
                    //Modify observable
                    Menu.AddRange(result.Items);
                }
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
            finally
            {
                IsRunning = false;
            }
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
