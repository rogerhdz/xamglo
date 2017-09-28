using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using XamarinPO.Services;

namespace XamarinPO.ViewModel.Application
{
    public class SettingsViewModel
    {
        private DialogService dialogService;

        public SettingsViewModel()
        {
            dialogService = new DialogService();
        }

        public string ServerUrl { get; set; }
        public ICommand SaveCommand => new RelayCommand(Save);

        private async void Save()
        {
            var url = this.ServerUrl;
            await dialogService.ShowMessage("Settings configured Correctly.","Settings");
        }
    }

    
}
