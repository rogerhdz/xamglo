using XamarinPO.Droid.Interfaces;
using XamarinPO.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NativeHelper))]
namespace XamarinPO.Droid.Interfaces
{
    public class NativeHelper : INativeHelper
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}