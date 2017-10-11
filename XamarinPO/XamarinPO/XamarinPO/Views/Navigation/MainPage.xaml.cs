using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.Services;
using XamarinPO.ViewModel;

namespace XamarinPO.Views.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        public DialogService DialogService { get; set; }
        public MainPage ()
		{
            DialogService = new DialogService();
			InitializeComponent();
		}

	    protected override async void OnAppearing()
	    {
	        await ((MainViewModel)BindingContext).InitApp();
	        base.OnAppearing();
            if (!CrossConnectivity.Current.IsConnected)
                await DialogService.ShowMessage("Check your connection", "Error");
	    }
    }
}