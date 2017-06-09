using System;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.PayPal.Enums;
using BridelleBelleMobileApplication.Models;

namespace BridelleBelleMobileApplication.PayPal.Interfaces
{
	public interface IPayPalManager
	{
		Task<PaymentResult> Buy(Magazine mag, Decimal tax, PaymentIntent intent = PaymentIntent.Sale);

		Task<ProfileSharingResult> AuthorizeProfileSharing();

		Task<ScanCardResult> ScanCard(CardIOLogo scannerLogo = CardIOLogo.PayPal);

		string ClientMetaDataId { get; }
	}
}
