using System;
using System.Diagnostics;
using PayPal.Forms.Abstractions;
using BridelleBelleMobileApplication.PayPal;
using BridelleBelleMobileApplication.PayPal.Interfaces;

namespace BridelleBelleMobileApplication.PayPal
{
	public class CrossPayPalManager
	{
		static PayPalConfiguration _config;
		public static void Init(PayPalConfiguration config)
		{
			IsInitialized = true;
			_config = config;
		}

		public static bool IsInitialized
		{
			get;
			private set;
		}

		static Lazy<BridelleBelleMobileApplication.PayPal.Interfaces.IPayPalManager> Implementation = new Lazy<BridelleBelleMobileApplication.PayPal.Interfaces.IPayPalManager>(() => CreatePayPalManager(),
			System.Threading.LazyThreadSafetyMode.PublicationOnly);

		public CrossPayPalManager() { }

		public static BridelleBelleMobileApplication.PayPal.Interfaces.IPayPalManager Current
		{
			get
			{
				if (!IsInitialized)
				{
					Debug.WriteLine("You Must Call PayPal.Forms.CrossPaypalManager.Init() before to use it");
					throw new NotImplementedException("You Must Call PayPal.Forms.CrossPaypalManager.Init() before to use it");
				}

				var ret = Implementation.Value;

				if(ret == null)
				{
					throw NotImplementedInReferenceAssembly();
				}

				return ret;
			}
		}

		public BridelleBelleMobileApplication.PayPal.Interfaces.IPayPalManager CreatePayPalManager()
		{
			#if PORTABLE
				return null;
			#else
				return new PayPalManagerImplementation(_config);
			#endif
		}

		internal static Exception NotImplementedInReferenceAssembly()
		{
			return new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
		}
	}
}