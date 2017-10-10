using System.Diagnostics;
using XamarinPO.Interfaces;

namespace XamarinPO.iOS.Interfaces
{
    public class NativeHelper : INativeHelper
    {
        public void CloseApp()
        {
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();
        }
    }
}