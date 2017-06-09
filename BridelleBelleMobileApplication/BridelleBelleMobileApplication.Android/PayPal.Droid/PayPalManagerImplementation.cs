using System;
using PayPal.Forms.Abstractions;
using System.Threading.Tasks;
using PayPal.Forms.Android;
using System.IO;
using Android.Graphics;
using Xamarin.Forms;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.PayPal;
using BridelleBelleMobileApplication.PayPal.Enums;

namespace BridelleBelleMobileApplication.Droid.PayPal.Droid
{
	public class PayPalManagerImplementation : BridelleBelleMobileApplication.PayPal.Interfaces.IPayPalManager
	{
		TaskCompletionSource<BridelleBelleMobileApplication.PayPal.PaymentResult> buyTcs;
		TaskCompletionSource<BridelleBelleMobileApplication.PayPal.ProfileSharingResult> apsTcs;
		TaskCompletionSource<BridelleBelleMobileApplication.PayPal.ScanCardResult> gcardTcs;

		public static PayPalManager Manager { get; private set; }

		public string ClientMetaDataId
		{
			get
			{
				return Manager.GetClientMetaDataId();
			}
		}

		public Task<BridelleBelleMobileApplication.PayPal.ProfileSharingResult>
			AuthorizeProfileSharing()
		{
			if(apsTcs != null)
			{
				apsTcs.TrySetCanceled();
				apsTcs.TrySetResult(null);
			}

			apsTcs = new TaskCompletionSource<BridelleBelleMobileApplication.PayPal.ProfileSharingResult>();
			Manager.AuthorizeProfileSharing
				(SendOnAuthorizeProfileSharingDidCancel,
				SendAuthorizeProfileSharingCompleted,
				SendOnPayPalPaymentError);

			return apsTcs.Task;
		}
		public Task<BridelleBelleMobileApplication.PayPal.PaymentResult> Buy(Magazine mag,Decimal tax,BridelleBelleMobileApplication.PayPal.Enums.PaymentIntent intent = BridelleBelleMobileApplication.PayPal.Enums.PaymentIntent.Sale)
		{
			if(buyTcs!= null)
			{
				buyTcs.TrySetCanceled();
				buyTcs.TrySetResult(null);
			}

			buyTcs = new TaskCompletionSource<BridelleBelleMobileApplication.PayPal.PaymentResult>();
			Manager.BuyItem(mag, tax, intent, SendOnPayPalPaymentDidCancel,
				SendOnPayPalPaymentCompleted,
				SendOnPayPalPaymentError);

			return buyTcs.Task;
		}

		public Task<BridelleBelleMobileApplication.PayPal.ScanCardResult> ScanCard(BridelleBelleMobileApplication.PayPal.Enums.CardIOLogo scannerLogo = BridelleBelleMobileApplication.PayPal.Enums.CardIOLogo.PayPal)
		{
			if (gcardTcs != null)
			{
				gcardTcs.TrySetCanceled();
				gcardTcs.TrySetResult(null);
			}

			gcardTcs = new TaskCompletionSource<BridelleBelleMobileApplication.PayPal.ScanCardResult>();
			Manager.RequestCardData(SendScanCardDidCancel, SendScanCardCompleted, scannerLogo);
			return gcardTcs.Task;
		}

		internal void SendOnAuthorizeProfileSharingDidCancel()
		{
			if (apsTcs != null)
			{
				apsTcs.TrySetResult(new BridelleBelleMobileApplication.PayPal.ProfileSharingResult(PayPalStatus.Cancelled));
			}
		}

		internal void SendOnPayPalPaymentDidCancel()
		{
			if(buyTcs != null)
			{
				buyTcs.TrySetResult(new BridelleBelleMobileApplication.PayPal.PaymentResult(
					PayPalStatus.Cancelled));
			}
		}

		internal void SendOnPayPalPaymentCompleted(string confirmationJSON)
		{
			if(buyTcs != null)
			{
				var serverResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<BridelleBelleMobileApplication.PayPal.PaymentResult.PayPalPaymentResponse>(confirmationJSON);
				buyTcs.TrySetResult(new BridelleBelleMobileApplication.PayPal.PaymentResult(PayPalStatus.Successful, String.Empty, serverResponse));
			}
		}

		internal void SendOnPayPalPaymentError(string errorMessage)
		{
			if(buyTcs != null)
			{
				buyTcs.TrySetResult(new BridelleBelleMobileApplication.PayPal.PaymentResult(
					PayPalStatus.Error, errorMessage));
			}
		}

		internal void SendScanCardDidCancel()
		{
			gcardTcs.SetResult(new BridelleBelleMobileApplication.PayPal.ScanCardResult(PayPalStatus.Cancelled));
		}

		internal void SendScanCardCompleted(Xamarin.PayPal.Android.CardIO.Payment.CreditCard cardInfo, Bitmap image)
		{
			var card = new BridelleBelleMobileApplication.PayPal.Card(
				(image != null),
				cardInfo.RedactedCardNumber,
				cardInfo.PostalCode,
				(int)cardInfo.ExpiryYear,
				(int)cardInfo.ExpiryMonth,
				cardInfo.Cvv,
				(BridelleBelleMobileApplication.PayPal.Enums.CreditCardType)Enum.Parse(typeof(BridelleBelleMobileApplication.PayPal.Enums.CreditCardType), cardInfo.CardType.Name, true), cardInfo.CardNumber,
				(image == null) ? null : ImageSource.FromStream(() =>
				{
					MemoryStream ms = new MemoryStream();
					image.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
					ms.Seek(0L, SeekOrigin.Begin);
					return ms;
				}));
			gcardTcs.SetResult(new BridelleBelleMobileApplication.PayPal.ScanCardResult(BridelleBelleMobileApplication.PayPal.Enums.PayPalStatus.Successful, card));
		}

		public PayPalManagerImplementation(BridelleBelleMobileApplication.PayPal.PayPalConfiguration config)
		{
			Manager = new PayPalManager(Xamarin.Forms.Forms.Context, config);
		}

		internal void SendOnAuthroizeSharigDidCancel()
		{
			if(apsTcs != null)
			{
				apsTcs.TrySetResult(new BridelleBelleMobileApplication.PayPal.ProfileSharingResult(BridelleBelleMobileApplication.PayPal.Enums.PayPalStatus.Cancelled));
			}
		}

		internal void SendAuthorizeProfileSharingCompleted(string confirmationJSON)
		{
			if(apsTcs != null)
			{
				var serverResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<BridelleBelleMobileApplication.PayPal.ProfileSharingResult.PayPalProfileSharingResponse>(confirmationJSON);
				apsTcs.TrySetResult(new BridelleBelleMobileApplication.PayPal.ProfileSharingResult(BridelleBelleMobileApplication.PayPal.Enums.PayPalStatus.Successful, string.Empty, serverResponse));
			}
		}
	}
}