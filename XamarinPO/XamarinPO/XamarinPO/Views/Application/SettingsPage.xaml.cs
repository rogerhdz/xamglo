using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.ViewModel.Application;

namespace XamarinPO.Views.Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsViewModel NewSettings { get; private set; }



        public SettingsPage()
        {
            NewSettings = new SettingsViewModel();
            BindingContext = NewSettings;

            InitializeComponent();
        }
    }
}