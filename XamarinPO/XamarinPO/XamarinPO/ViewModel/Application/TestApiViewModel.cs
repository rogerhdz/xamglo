using GalaSoft.MvvmLight.Command;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinPO.Helpers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinPO.Services;

namespace XamarinPO.ViewModel.Application
{
    public class TestApiViewModel : INotifyPropertyChanged
    {
        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes

        private string _requestText;
        private string _responseText;
        private string _apiStatus;
        #endregion

        #region Properties
        public string RequestText
        {
            get => _requestText;
            set
            {
                if (_requestText == value) return;
                _requestText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestText)));
            }
        }
        public string ResponseText
        {
            get => _responseText;
            set
            {
                if (_responseText == value) return;
                _responseText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResponseText)));
            }
        }
        public string ApiStatus
        {
            get => _apiStatus;
            set
            {
                if (_apiStatus == value) return;
                _apiStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ApiStatus)));
            }
        }
        public HttpManagerConfiguration ManagerConfiguration { get; set; }
        public HttpManagerResult<string> Result { get; set; }
        public DialogService DialogService { get; set; }
        #endregion

        #region Constructors
        public TestApiViewModel()
        {
            ManagerConfiguration = new HttpManagerConfiguration()
            {
                Server = ApplicationPropertiesManager.Load<Settings>("ApiServer").ServerUrl
            };
            ResponseText = string.Empty;
            TestConnection();
        }


        #endregion

        #region Methods

        private async void TestConnection()
        {
            ManagerConfiguration.Method = "/api/testapi/testconnection";
            ApiStatus = await PostAsync(string.Empty);
        }

        private void SetRequestStatus()
        {
            ApiStatus = Result.Success ? "Request Success" : "Request Failed";
        }

        public async Task<string> TestRequest()
        {
            ManagerConfiguration.Method = "/api/testapi/testrequest";
            return await PostAsync(RequestText);
        }

        private async void TestTimeout()
        {
            ManagerConfiguration.Method = "/api/testapi/testtimeout";
            ApiStatus = await GetAsync();
        }

        private async Task<string> PostAsync(string postDataString)
        {
            try
            {
                var manager = new HttpManager<string>();
                var stringContent = new StringContent($@"""{postDataString}""", System.Text.Encoding.UTF8, "application/json");
                Result = await manager.HttpJsonPost(ManagerConfiguration, stringContent);
            }
            catch (Exception ex)
            {
                ApiStatus = ex.Message;
            }
            return Result.ObjectResult.ToString();
        }

        private async Task<string> GetAsync()
        {
            try
            {
                var manager = new HttpManager<string>();
                Result = await manager.HttpJsonGet(ManagerConfiguration);
            }
            catch (Exception ex)
            {
                ApiStatus = ex.Message;
            }
            return Result.ObjectResult.ToString();
        }

        
        #endregion

        #region Commands

        public ICommand Share => new RelayCommand(ShareCommand);
        public ICommand SendRequestCommand => new RelayCommand(SendRequest);
        public ICommand SendTimeoutCommand => new RelayCommand(TestTimeout);
        private async void ShareCommand()
        {
            if (string.IsNullOrEmpty(ResponseText))
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "Nothing to share", "Accept");
            else
            {
                MessagingCenter.Send(ResponseText, "Share");
            }
        }
        private async void SendRequest()
        {
            if (string.IsNullOrEmpty(RequestText))
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "You must enter a value in Request Text", "Accept");
            else
            {
                ResponseText = await TestRequest();
                SetRequestStatus();
            }
        }
        #endregion
    }
}
