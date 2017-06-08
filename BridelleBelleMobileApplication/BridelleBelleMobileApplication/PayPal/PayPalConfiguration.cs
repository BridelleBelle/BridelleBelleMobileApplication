using PayPal.Forms.Abstractions.Enum;

namespace BridelleBelleMobileApplication.PayPal
{
	public class PayPalConfiguration
	{
		public BridelleBelleMobileApplication.PayPal.Enums.PayPalEnvironment Environment { get; private set; }
		public string PayPalKey { get; private set; }
		public string PhoneCountryCode { get; set; }
		public string Language { get; set; }
		public string MerchantName { get; set; }
		public string MerchantPrivacyPolicyUri { get; set; }
		public string MerchantUserAgreementUri { get; set; }
		public bool AcceptCreditCards { get; set; }

		//public ShippingAddressOption ShippingAddressOption { get; set; }

		public PayPalConfiguration(BridelleBelleMobileApplication.PayPal.Enums.PayPalEnvironment environment, string idEnvironment)
		{
			this.Environment = environment;
			this.PayPalKey = idEnvironment;
		}
	}
}
