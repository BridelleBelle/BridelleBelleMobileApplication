using System;
using BridelleBelleMobileApplication.PayPal;
using BridelleBelleMobileApplication.PayPal.Enums;

namespace BridelleBelleMobileApplication.PayPal
{
	public class ScanCardResult
	{
		public PayPalStatus Status { get; private set; }
		public Card Card { get; private set; }

		public ScanCardResult(PayPalStatus status, Card card = null)
		{
			Status = status;
			Card = card;
		}
	}
}
