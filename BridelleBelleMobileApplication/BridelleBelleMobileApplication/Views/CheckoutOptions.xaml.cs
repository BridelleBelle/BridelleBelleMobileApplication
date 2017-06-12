using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

using BridelleBelleMobileApplication.Helpers;
using BridelleBelleMobileApplication.Models;

using PayPal.Forms.Abstractions.Enum;
namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckoutOptions : PopupPage
	{
		private Magazine Magazine;
		public CheckoutOptions(Magazine mag)
		{
			InitializeComponent();
			this.Magazine = mag;
			paymentOptions.ItemsSource = new List<string> { "PayPal", "Voucher" };
		}

		public async Task HandlePaymentOption(object sender, SelectedItemChangedEventArgs e)
		{
			switch (e.SelectedItem.ToString())
			{
				case "PayPal":
					await Pay();
					break;
				case "Voucher":
					break;
			}
		}

		public async Task Pay()
		{
			var payPalPayment = new PayPalPayments();
			var result = await payPalPayment.Pay(this.Magazine);

			switch (result)
			{
				case PayPalStatus.Successful:
					DisplayAlert("Payment Successful", "Payment was successful. " + this.Magazine.Name + " was added to your inventory and is now fully viewable.", "OK");
					await Navigation.PopAllPopupAsync();
					break;
				case PayPalStatus.Error:
					DisplayAlert("Payment Error", "Error during payment. Please check your credentials.", "OK");
					await Navigation.PopAllPopupAsync();
					break;
				case PayPalStatus.Cancelled:
					DisplayAlert("Payment Cancelled", "Payment was cancelled.", "OK");
					break;
			}
		}
	}
}
