using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPO.Interfaces;
using XamarinPO.Views.Application;
using XamarinPO.Views.Client;
using XamarinPO.Views.Order;

namespace XamarinPO.Services
{
    public class NavigationService
    {
        public async void Navigate(string PageName)
        {
            App.Master.IsPresented = false;
            switch (PageName)
            {
                case "ClientPage":
                    await Navigate(new ClientPage());
                    break;
                case "MainPage":
                    await App.Navigator.PopToRootAsync();
                    break;
                case "NewOrderPage":
                    await Navigate(new NewOrderPage());
                    break;
                case "SettingsPage":
                    await Navigate(new SettingsPage());
                    break;
                case "AboutUsPage":
                    await Navigate(new AboutUs());
                    break;
                case "CloseApp":
                    DependencyService.Get<INativeHelper>().CloseApp();
                    break;
                case "TestApiPage":
                    await Navigate(new TestApiPage());
                    break;
                default:
                    break;
            }
        }
        private static async Task Navigate<T>(T Page) where T : Page
        {
            NavigationPage.SetBackButtonTitle(Page, "Back");

            await App.Navigator.PushAsync(Page);
        }
    }
}
