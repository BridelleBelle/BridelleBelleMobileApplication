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
					PayPalEnvironment.NoNetwork,
					"AcRNDPqkr4qxK0XD8d0slrnAToJKZ8Q_SbxXrRSptMTt8kPO04JPX7vDscm22VfvxcCiHAVj474FG3uU"
				)
				{
					AcceptCreditCards = true,
					MerchantName = "bellebridalapp",
					MerchantPrivacyPolicyUri = "https://www.sandbox.paypal.com/uk/webapps/mpp/ua/upcoming-policies-full",
					MerchantUserAgreementUri = "https://www.sandbox.paypal.com/webapps/mpp/ua/useragreement-full?locale.x=en_GB"
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

