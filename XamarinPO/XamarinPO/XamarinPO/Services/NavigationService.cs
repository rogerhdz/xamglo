using System.Threading.Tasks;
using Xamarin.Forms;
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
