using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinPO.Services;
using XamarinPO.Views.Client;

namespace XamarinPO.ViewModel.Menu
{
    public class MenuItemViewModel
    {

        #region Properties
        public NavigationService NavigationService { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion
        public MenuItemViewModel()
        {
            NavigationService = new NavigationService();
        }

        public ICommand NavigateCommand
        {
            get { return new RelayCommand(() => NavigationService.Navigate(PageName)); }
        }
    }
}
