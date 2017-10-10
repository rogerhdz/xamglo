﻿using GalaSoft.MvvmLight.Command;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinPO.Helpers;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinPO.ViewModel.Application
{
    public class TestApiViewModel : INotifyPropertyChanged
    {
        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        string _requestText;
        string _responseText;
        string _apiStatus;
        #endregion

        #region Properties
        public string RequestText
        {
            get
            {
                return _requestText;
            }
            set
            {
                if (_requestText != value)
                {
                    _requestText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestText)));
                }
            }
        }
        public string ResponseText
        {
            get
            {
                return _responseText;
            }
            set
            {
                if (_responseText != value)
                {
                    _responseText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResponseText)));
                }
            }
        }
        public string ApiStatus
        {
            get
            {
                return _apiStatus;
            }
            set
            {
                if (_apiStatus != value)
                {
                    _apiStatus = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ApiStatus)));
                }
            }
        }
        public HttpManagerConfiguration ManagerConfiguration { get; set; }
        public HttpManagerResult<string> Result { get; set; }
        #endregion

        #region Constructors
        public TestApiViewModel()
        {
            ManagerConfiguration = new HttpManagerConfiguration()
            {
                Server = "http://xamarinpoapi.azurewebsites.net"
            };
            ResponseText = string.Empty;
            TestConnection();
        }


        #endregion

        #region Methods
        async void TestConnection()
        {
            ManagerConfiguration.Method = "/api/testapi/testconnection";
            ApiStatus = await PostAsync(string.Empty);
        }

        public async Task<string> TestRequest()
        {
            ManagerConfiguration.Method = "/api/testapi/testrequest";
            return await PostAsync(RequestText);
        }

        private async Task<string> PostAsync(string postDataString)
        {
            //Create manager
            var manager = new HttpManager<string>();
            var stringContent = new StringContent(string.Format(@"""{0}""", postDataString), System.Text.Encoding.UTF8, "application/json");
            Result = await manager.HttpJsonPost(ManagerConfiguration, stringContent);
            return Result.ObjectResult.ToString();
        }
        #endregion

        #region Commands

        public ICommand Share {
            get { return new RelayCommand(ShareCommand); }
        }
        public ICommand SendRequestCommand
        {
            get { return new RelayCommand(SendRequest); }
        }

        async void ShareCommand()
        {
            if(string.IsNullOrEmpty(this.ResponseText))
                await App.Current.MainPage.DisplayAlert("Error", "Nothing to share", "Accept");
            else
            {
                MessagingCenter.Send<string>(this.ResponseText, "Share");
            }
        }
        async void SendRequest()
        {
            if (string.IsNullOrEmpty(RequestText))
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a value in Request Text", "Accept");
            else
            {
                ResponseText = await TestRequest();
                ApiStatus = "Request successful";
            }
        }
        #endregion
    }
}
