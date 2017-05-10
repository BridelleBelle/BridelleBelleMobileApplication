using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MagazineViewer : ContentPage
	{
		public MagazineViewer ()
		{
			//InitializeComponent ();
			var ImageId = 22;
			BindingContext = new PageView();
		}
	}
}
