using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BridelleBelleMobileApplication.Models;

namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPageView : MasterDetailPage
	{
		public MasterPageView (Magazine mag)
		{
			InitializeComponent ();
			this.Master = new PageView(mag);
			this.Detail = new NavigationPage(new AdsView());
		}
	}
}
