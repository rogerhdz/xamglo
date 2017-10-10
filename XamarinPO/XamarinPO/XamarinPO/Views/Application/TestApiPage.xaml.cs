using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.ViewModel.Application;

namespace XamarinPO.Views.Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestApiPage : ContentPage
    {
        public TestApiViewModel Model { get; set; }
        public TestApiPage()
        {
            Model = new TestApiViewModel();
            this.BindingContext = Model;
            InitializeComponent();
        }
    }
}