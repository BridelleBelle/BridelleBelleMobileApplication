using System;

namespace BridelleBelleMobileApplication.PayPal.Enums
{
	public enum CreditCardType : long
	{
		UnRecognize,
		Ambiguous,
		Amex = 51L,
		Jcb = 74L,
		Visa = 52L,
		Mastercard,
		Discover
	}
}
