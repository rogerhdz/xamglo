using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.Services;
using XamarinPO.ViewModel.Application;

namespace XamarinPO.Views.Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestApiPage : ContentPage
    {
        public TestApiViewModel Model { get; set; }
        public DialogService DialogService { get; set; }
        public TestApiPage()
        {
            Model = new TestApiViewModel();
            DialogService = new DialogService();
            this.BindingContext = Model;
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                if (!CrossConnectivity.Current.IsConnected)
                    await DialogService.ShowMessage("Check your connection", "Error");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!CrossConnectivity.Current.IsConnected)
                await DialogService.ShowMessage("Check your connection", "Error");
        }
    }
}