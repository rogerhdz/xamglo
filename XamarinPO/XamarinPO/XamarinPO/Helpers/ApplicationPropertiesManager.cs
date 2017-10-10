using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinPO.Helpers
{
    public static class ApplicationPropertiesManager
    {
        public static async Task Save<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }

        public static T Load<T>(string key)
        {
            return (T)Application.Current.Properties[key];
        }

        public static void ClearAll()
        {
            Application.Current.Properties.Clear();
        }

        // To save your property
        //await SaveApplicationProperty("isLoggedIn", true);

        // To load your property
        //bool isLoggedIn = LoadApplicationProperty<bool>("isLoggedIn");
    }


}
