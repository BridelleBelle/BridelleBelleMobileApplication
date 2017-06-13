using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Views;
using BridelleBelleMobileApplication.Types;
namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : Xamarin.Forms.TabbedPage
	{
		public HomePage (BridelleBelleMobileApplication.Types.TabbedPage tab)
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);
			if(tab == BridelleBelleMobileApplication.Types.TabbedPage.UserMagazines)
			{
				CurrentPage = usermags;
			}
		}
	}
}
