using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPO.ViewModel.Application;

namespace XamarinPO.Views.Order
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewOrderPage : ContentPage
	{
		public NewOrderPage ()
		{
			InitializeComponent ();   
		}
	}
}