using System;
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
            T propertyValue;
            try
            {
                 propertyValue= (T)Application.Current.Properties[key];
            }
            catch (Exception)
            {
                propertyValue = (T)Activator.CreateInstance(typeof(T));
            }
            return propertyValue;
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
