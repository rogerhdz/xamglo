using System.Collections.Generic;
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
        private DialogService dialogService;

        public SettingsViewModel()
        {
            dialogService = new DialogService();
            
            SetToAppServerUrl();
        }

        public ICommand SaveCommand => new RelayCommand(Save);

        private async void Save()
        {
            IsRunning = true;
            Result = "Saving Settings";

            //Get settings from application property
            var settingsObj = ApplicationPropertiesManager.LoadApplicationProperty<Settings>("settings");

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
                await dialogService.ShowMessage("Settings configured Correctly.", "Settings");
            else
                await dialogService.ShowMessage("Error configuring settings.", "Settings");

            //Read te current configuration on file
            //string configText = DependencyService.Get<IFilesManager>().LoadText("App.config");

            //If reading was successfull 
            //if (configText != string.Empty)
            //{
                
                //create new configuration and update local file
                //string newConfigText = configText.Replace(settingsObj.ServerUrl, ServerUrl);
                //DependencyService.Get<IFilesManager>().SaveText("App.config", newConfigText);
                //SetToAppServerUrl();
                ////update old server if possible
                //Set server to update
                //var config = new HttpManagerConfiguration
                //{
                //    Method = "/Tables/Settings",
                //    Server = settingsObj.ServerUrl
                //};
                // var manager = new HttpManager<Settings>();
                //HttpManagerResult<Settings> result = await manager.HttpPatchAzure(config, settingsObj);
                //Result = result.Message;
                //if (result.Success)
                //    await dialogService.ShowMessage("Settings configured Correctly.", "Settings");
                //else
                //    await dialogService.ShowMessage("Error configuring settings.", "Settings");
            //}
            //else
            //{
            //    await dialogService.ShowMessage("Error configuring settings.", "Settings");
            //}
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

        async void SetToAppServerUrl()
        {
            //Obtains url to point from app.config the first time, then obtains url from server
            settings = await GetApiServerUrl();
            //Set url got from server and applies it to an application property to avoid going server each request
            await ApplicationPropertiesManager.SaveApplicationProperty("settings", settings);
            //Set url to two-way-binding property to show it in front
            ServerUrl = settings.ServerUrl;
        }

        public class Settings
        {
            public string Id { get; set; }
            public string ServerUrl { get; set; }
        }
    }


}
