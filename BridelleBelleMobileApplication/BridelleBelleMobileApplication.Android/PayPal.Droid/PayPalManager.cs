using System;
using Android.Content;
using Xamarin.PayPal.Android;
using Java.Math;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Org.Json;
using Xamarin.PayPal.Android.CardIO.Payment;
using Android.Graphics;
using System.Globalization;
using PayPal.Forms.Abstractions.Enum;
using BridelleBelleMobileApplication.PayPal.Enums;
using BridelleBelleMobileApplication.Models;

namespace BridelleBelleMobileApplication.Droid.PayPal.Droid
{
	public class PayPalManager
	{
		Context Context;

		private static string CONFIG_ENVIRONMENT;

		private static string CONFIG_CLIENT_ID = "credential from developer.paypal.com";

		public static int REQUEST_CODE_PAYMENT = 1;
		public static int REQUEST_CODE_FUTURE_PAYMENT = 2;
		public static int REQUEST_CODE_PROFILE_SHARING = 3;
		public static int REQUEST_CODE_CARD_SCAN = 4;

		private PayPalConfiguration config;

		BridelleBelleMobileApplication.PayPal.PayPalConfiguration _bbconfiguration;

		public PayPalManager(Context context, BridelleBelleMobileApplication.PayPal.PayPalConfiguration bbconfiguration)
		{
			_bbconfiguration = bbconfiguration;
			Context = context;

			switch (bbconfiguration.Environment)
			{
				case BridelleBelleMobileApplication.PayPal.Enums.PayPalEnvironment.NoNetwork:
					CONFIG_ENVIRONMENT = PayPalConfiguration.EnvironmentNoNetwork;
					break;
				case BridelleBelleMobileApplication.PayPal.Enums.PayPalEnvironment.Production:
					CONFIG_ENVIRONMENT = PayPalConfiguration.EnvironmentProduction;
					break;
				case BridelleBelleMobileApplication.PayPal.Enums.PayPalEnvironment.Sandbox:
					CONFIG_ENVIRONMENT = PayPalConfiguration.EnvironmentSandbox;
					break;
			}

			CONFIG_CLIENT_ID = bbconfiguration.PayPalKey;

			config = new PayPalConfiguration()
				.Environment(CONFIG_ENVIRONMENT)
				.ClientId(CONFIG_CLIENT_ID)
				.AcceptCreditCards(bbconfiguration.AcceptCreditCards)
				.MerchantName(bbconfiguration.MerchantName)
				.MerchantPrivacyPolicyUri(global::Android.Net.Uri.Parse(bbconfiguration.MerchantPrivacyPolicyUri))
				.MerchantUserAgreementUri(global::Android.Net.Uri.Parse(bbconfiguration.MerchantUserAgreementUri));

			if (!String.IsNullOrEmpty(bbconfiguration.Language))
			{
				config = config.LanguageOrLocale(bbconfiguration.Language);
			}
			if (!String.IsNullOrEmpty(bbconfiguration.PhoneCountryCode))
			{
				config = config.DefaultUserPhoneCountryCode(bbconfiguration.PhoneCountryCode);
			}

			var intent = new Intent(Context, typeof(PayPalService));
			intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);
			Context.StartService(intent);
		}

		private PayPalPayment GetThingToBuy(string paymentIntent, double amount, Magazine mag)
		{
			return new PayPalPayment(new BigDecimal(amount), "GBP", mag.ToString(),paymentIntent);
		}

		private PayPalOAuthScopes GetOAuthScropes()
		{
			var scopes = new HashSet<string>();
			scopes.Add(PayPalOAuthScopes.PaypalScopeOpenid);
			scopes.Add(PayPalOAuthScopes.PaypalScopeEmail);
			scopes.Add(PayPalOAuthScopes.PaypalScopeAddress);
			scopes.Add(PayPalOAuthScopes.PaypalScopePhone);
			return new PayPalOAuthScopes(scopes.ToList());
		}

		Action OnCancelled;
		Action<string> OnSuccess;
		Action<string> OnError;

		public void BuyItem(Magazine mag, Decimal xftax, BridelleBelleMobileApplication.PayPal.Enums.PaymentIntent xfintent, Action onCancelled, Action<string> onSuccess, Action<string> onError)
		{
			OnCancelled = onCancelled;
			OnSuccess = onSuccess;
			OnError = onError;
			var amount = new BigDecimal(RoundNumber((double)mag.Price)).Add(new BigDecimal(RoundNumber((double)xftax)));
			var paymentIntent = String.Empty;
			switch (xfintent)
			{
				case BridelleBelleMobileApplication.PayPal.Enums.PaymentIntent.Authorize:
					paymentIntent = PayPalPayment.PaymentIntentAuthorize;
					break;
				case BridelleBelleMobileApplication.PayPal.Enums.PaymentIntent.Order:
					paymentIntent = PayPalPayment.PaymentIntentOrder;
					break;
				default:
				case BridelleBelleMobileApplication.PayPal.Enums.PaymentIntent.Sale:
					paymentIntent = PayPalPayment.PaymentIntentSale;
					break;
			}

			var payment = new PayPalPayment(amount, "GBP", mag.Name, paymentIntent);

			var intent = new Intent(Context, typeof(PaymentActivity));
			intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);
			intent.PutExtra(PaymentActivity.ExtraPayment, payment);
			(Context as Activity).StartActivityForResult(intent, REQUEST_CODE_PAYMENT);
		}

		public string GetClientMetaDataId()
		{
			return PayPalConfiguration.GetClientMetadataId(Context);
		}

		public void AuthorizeProfileSharing(Action onCancelled, Action<string> onSuccess, Action<string> onError)
		{
			OnCancelled = onCancelled;
			OnSuccess = onSuccess;
			OnError = onError;

			Intent intent = new Intent(Context, typeof(PayPalProfileSharingActivity));

			intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);

			intent.PutExtra(PayPalProfileSharingActivity.ExtraRequestedScopes, GetOAuthScropes());

			(Context as Activity).StartActivityForResult(intent, REQUEST_CODE_PROFILE_SHARING);
		}

		Action RetrieveCardCancelled;
		Action<Xamarin.PayPal.Android.CardIO.Payment.CreditCard, Bitmap> RetrieveCardSuccess;

		public void RequestCardData(Action onCancelled,Action<Xamarin.PayPal.Android.CardIO.Payment.CreditCard,Bitmap> onSuccess,BridelleBelleMobileApplication.PayPal.Enums.CardIOLogo scannerLogo)
		{
			RetrieveCardCancelled = onCancelled;
			RetrieveCardSuccess = onSuccess;

			var intent = new Intent(Context, typeof(CardIOActivity));

			switch(scannerLogo)
			{
				case BridelleBelleMobileApplication.PayPal.Enums.CardIOLogo.CardIO:
					intent.PutExtra(CardIOActivity.ExtraHideCardioLogo, false);
					intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);
					break;
				case BridelleBelleMobileApplication.PayPal.Enums.CardIOLogo.None:
					intent.PutExtra(CardIOActivity.ExtraHideCardioLogo, true);
					intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, false);
					break;
			}

			intent.PutExtra(CardIOActivity.ExtraReturnCardImage, true);
			intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true);
			intent.PutExtra(CardIOActivity.ExtraRequireCvv, true);

			(Context as Activity).StartActivityForResult(intent, REQUEST_CODE_CARD_SCAN);
		}

		public void Destroy()
		{
			Context.StopService(new Intent(Context, typeof(PayPalService)));
		}

		public void OnActivityResult(int requestCode, Result resultCode,global::Android.Content.Intent data)
		{
			if (requestCode == PayPalManager.REQUEST_CODE_PAYMENT)
			{
				if (resultCode == Result.Ok)
				{
					var confirm = (PaymentConfirmation)data.GetParcelableExtra(PaymentActivity.ExtraResultConfirmation);

					if (confirm != null)
					{
						try
						{
							OnSuccess?.Invoke(confirm.ToJSONObject().ToString());
							OnSuccess = null;
						}
						catch (JSONException e)
						{
							OnError?.Invoke("an extremely unlikely failure occured: " + e.Message);
							OnError = null;
							System.Diagnostics.Debug.WriteLine("an extremely unlikely failured occured: " + e.Message);
						}
					}

					OnError?.Invoke("Unknown Error");
					OnError = null;
				}
				else if (resultCode == Result.Canceled)
				{
					OnCancelled?.Invoke();
					OnCancelled = null;
					System.Diagnostics.Debug.WriteLine("The user canceled");
				}
				else if ((int)resultCode == PaymentActivity.ResultExtrasInvalid)
				{
					OnError?.Invoke("An invalid payment or paypalconfiguration was submitted. please check the docs.");
					OnError = null;
					System.Diagnostics.Debug.WriteLine("An invalid Payment or PayPalConfiguration was submitted. Please see the docs");
				}
			}
			else if (requestCode == REQUEST_CODE_PROFILE_SHARING)
			{
				if (resultCode == Result.Ok)
				{
					PayPalAuthorization auth =
						(PayPalAuthorization)data.GetParcelableExtra(PayPalProfileSharingActivity.ExtraResultAuthorization);
					if (auth != null)
					{
						try
						{
							OnSuccess?.Invoke(auth.ToJSONObject().ToString());
							OnSuccess = null;
						}
						catch (JSONException e)
						{
							System.Diagnostics.Debug.WriteLine("an extremely unlikely failure occurred: " + e.Message);
						}
					}
					OnError?.Invoke("Unknown Error");
					OnError = null;
				}
				else if (resultCode == Result.Canceled)
				{
					OnCancelled?.Invoke();
					OnCancelled = null;
					System.Diagnostics.Debug.WriteLine("The user canceled.");
				}
				else if ((int)resultCode == PayPalFuturePaymentActivity.ResultExtrasInvalid)
				{
					OnError?.Invoke("Probably the attempt to previously start the PayPalService had an invalid PayPalConfiguration. Please see the docs.");
					OnError = null;
					System.Diagnostics.Debug.WriteLine(
						"Probably the attempt to previously start the PayPalService had an invalid PayPalConfiguration. Please see the docs.");
				}
			}
			else if (requestCode == REQUEST_CODE_CARD_SCAN)
			{
				if (data == null)
				{
					RetrieveCardCancelled?.Invoke();
					RetrieveCardCancelled = null;
					System.Diagnostics.Debug.WriteLine("The user canceled.");
					return;
				}
				var card = (CreditCard)data.GetParcelableExtra(CardIOActivity.ExtraScanResult);
				if (card != null)
				{
					RetrieveCardSuccess?.Invoke(card, CardIOActivity.GetCapturedCardImage(data));
					RetrieveCardSuccess = null;
				}
				else
				{
					RetrieveCardCancelled?.Invoke();
					RetrieveCardCancelled = null;
					System.Diagnostics.Debug.WriteLine("The user canceled.");
				}
			}
		}

		string RoundNumber(double num)
		{
			var s = string.Format(CultureInfo.InvariantCulture, "{0:0.00}", num);

			if (s.EndsWith("00"))
			{
				return ((int)num).ToString();
			}
			else
			{
				return s;
			}
		}

	}
}