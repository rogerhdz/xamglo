namespace XamarinPO.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = cultureInfo;
            DependencyService.Get<ILocalize>().SetLocale(cultureInfo);
        }

        public static string GetCulture()
        {
            var cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            return cultureInfo.ToString()?.Substring(0,2);
        }

        public static string Settings => Resource.Settings;
        public static string TestApi => Resource.TestApi;
        public static string Close => Resource.Close;
    }
}
