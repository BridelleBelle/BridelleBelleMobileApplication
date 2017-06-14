using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Views;
namespace BridelleBelleMobileApplication.Views.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
	{
		public MasterPage (BridelleBelleMobileApplication.Types.TabbedPage tab)
		{
			InitializeComponent ();
			this.Master = new SliderMenu();
			this.Detail = new NavigationPage(new HomePage(tab));
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
