﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using PCLAppConfig;
using Xamarin.Forms;
using XamarinPO.Helpers;
using XamarinPO.Interfaces;
using XamarinPO.Services;

namespace XamarinPO.ViewModel.Application
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string ServerUrl
        {
            get => _serverUrl;
            set
            {
                if (_serverUrl != value)
                {
                    _serverUrl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ServerUrl)));
                }
            }
        }
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning == value) return;
                _isRunning = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
            }
        }
        public string Result
        {
            get => _result;
            set
            {
                if (_result == value) return;
                _result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }
        #endregion 

        #region Attributes
        bool _isRunning;
        string _result;
        string _serverUrl;
        #endregion

        public Settings settings { get; set; }
        private readonly DialogService dialogService;

        public SettingsViewModel()
        {
            dialogService = new DialogService();
            GetApiServerUrl();
        }

        public ICommand SaveCommand => new RelayCommand(Save);

        private async void Save()
        {
            IsRunning = true;
            Result = "Saving Settings";

            //Get settings from application property
            var settingsObj = ApplicationPropertiesManager.Load<Settings>("settings");

            //Update the new api end point
            settingsObj.ServerUrl = ServerUrl;

            //Set server to update
            var config = new HttpManagerConfiguration
            {
                Method = "/Tables/Settings",
                Server = settingsObj.ServerUrl
            };
            var manager = new HttpManager<Settings>();
            HttpManagerResult<Settings> result = await manager.HttpPatchAzure(config, settingsObj);
            Result = result.Message;
            if (result.Success)
            {
                await ApplicationPropertiesManager.Save("settings", settingsObj);
                await dialogService.ShowMessage("Settings configured Correctly.", "Settings");
            }
            else
                await dialogService.ShowMessage("Error configuring settings.", "Settings");

            IsRunning = false;
        }

        async Task<Settings> GetApiServerUrl()
        {
            //Instance result observable list
            settings = new Settings();
            IsRunning = true;
            Result = "Loading Settings";
            //Create configurator to consume api
            var config = new HttpManagerConfiguration
            {
                Method = "/Tables/Settings",
                Server = ConfigurationManager.AppSettings["ServerUrl"]
            };


            //Create manager
            var manager = new HttpManager<Settings>();
            //Get object with items, isSuccess and message
            HttpManagerResult<Settings> result = await manager.HttpGetAzureList(config);
            Result = result.Message;
            var settingsObj = ((List<Settings>)result.ObjectResult)[0];
            if (!result.Success)
            {
                settingsObj.ServerUrl = "Error ocurred";
            }
            IsRunning = false;
            return settingsObj;
        }

        public class Settings
        {
            public string Id { get; set; }
            public string ServerUrl { get; set; }
        }
    }


}
