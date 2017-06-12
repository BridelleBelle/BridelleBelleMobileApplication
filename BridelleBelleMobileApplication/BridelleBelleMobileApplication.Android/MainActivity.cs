using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
namespace BridelleBelleMobileApplication.Droid
{
	[Activity (Label = "BridelleBelleMobileApplication", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			PayPal.Forms.CrossPayPalManager.Init(
				new PayPalConfiguration(
					PayPalEnvironment.Sandbox,
					"AU-mQOLVcNAQmM5lIk7S1TVZrOlZwJaQriFZ0bOxKj8QK6IzNIS4ztxqYt0it4hSGD_9DL3__vN6_84a"
				)
				{
					AcceptCreditCards = true,
					MerchantName = "test",
					MerchantPrivacyPolicyUri = "https://www.sandbox.paypal.com/uk/webapps/mpp/ua/upcoming-policies-full",
					MerchantUserAgreementUri = "https://www.sandbox.paypal.com/webapps/mpp/ua/useragreement-full?locale.x=en_GB",
					// OPTIONAL - Language: Default languege for PayPal Plug-In
					Language = "ang",

					// OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
					PhoneCountryCode = "44",
				}
			);
			LoadApplication (new BridelleBelleMobileApplication.App ());	
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			PayPal.Forms.PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			PayPal.Forms.PayPalManagerImplementation.Manager.Destroy();
		}

	}
}

