using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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