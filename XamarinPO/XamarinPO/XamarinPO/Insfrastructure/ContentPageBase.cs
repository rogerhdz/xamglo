using Plugin.Connectivity;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.Services;
using XamarinPO.ViewModel.Application;

namespace XamarinPO.Insfrastructure
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract class ContentPageBase : ContentPage  
    {
        public DialogService DialogService { get; set; }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            await CheckInternetConnection();
        }
        private async Task CheckInternetConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
                await DialogService.ShowMessage("Check your connection", "Error");
        }
    }
}




