using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinPO.Helpers
{
    class ApplicationPropertiesManager
    {
        private async Task SaveApplicationProperty<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }

        private T LoadApplicationProperty<T>(string key)
        {
            return (T)Application.Current.Properties[key];
        }

        // To save your property
        //await SaveApplicationProperty("isLoggedIn", true);

        // To load your property
        //bool isLoggedIn = LoadApplicationProperty<bool>("isLoggedIn");
    }


}
