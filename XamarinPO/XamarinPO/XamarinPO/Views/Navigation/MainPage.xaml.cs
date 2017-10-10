using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.ViewModel;

namespace XamarinPO.Views.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent();
		}

	    protected override async void OnAppearing()
	    {
	        await ((MainViewModel)BindingContext).InitApp();
	        base.OnAppearing();
	    }
    }
}