using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using PCLAppConfig;
using XamarinPO.Helpers;
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
        public Settings settings { get; set; }
        private readonly DialogService dialogService;
        #endregion 

        #region Attributes
        bool _isRunning;
        string _result;
        string _serverUrl;
        #endregion

        #region Constructors
        public SettingsViewModel()
        {
            dialogService = new DialogService();
            ServerUrl = ApplicationPropertiesManager.Load<Settings>("ApiServer").ServerUrl;
        }
        #endregion

        #region Methods
        private async void Save()
        {
            IsRunning = true;
            Result = "Saving Settings";

            //Get settings from application property
            var settingsObj = ApplicationPropertiesManager.Load<Settings>("ApiServer");
            settingsObj.ServerUrl = ServerUrl;

            //Set server to update
            var config = new HttpManagerConfiguration
            {
                Method = "/Tables/Settings",
                Server = ConfigurationManager.AppSettings["SettingsServer"]
            };
            var manager = new HttpManager<Settings>();
            HttpManagerResult<Settings> result = await manager.HttpPatchAzure(config, settingsObj);
            Result = result.Message;
            if (result.Success)
            {
                await ApplicationPropertiesManager.Save("ApiServer", settingsObj);
                await dialogService.ShowMessage("Settings configured Correctly.", "Settings");
            }
            else
                await dialogService.ShowMessage("Error configuring settings.", "Settings");

            IsRunning = false;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand => new RelayCommand(Save);
        #endregion
    }

    #region HelperClasses
    public class Settings
    {
        public string Id { get; set; }
        public string ServerUrl { get; set; }
    }
    #endregion

}
