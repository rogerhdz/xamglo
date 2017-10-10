using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using PCLAppConfig;
using Xamarin.Forms;

namespace XamarinPO.Droid
{
    [Activity(Label = "XamarinPO", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int ShareTextId = 1000;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);
            LoadApplication(new App());

            MessagingCenter.Subscribe<string>(this, "Share", Share, null);
        }

        async void Share(string messageSource)
        {

            var sharingIntent = new Intent(Intent.ActionSend);
            sharingIntent.SetType("text/plain");
            sharingIntent.PutExtra(Intent.ExtraSubject, "Share Via");
            sharingIntent.PutExtra(Intent.ExtraText, messageSource);
            sharingIntent.PutExtra(Intent.ExtraTitle, "Title");

            var intentChooser = Intent.CreateChooser(sharingIntent, "Sharing Option");

            StartActivityForResult(intentChooser, ShareTextId);
        }
    }
}

