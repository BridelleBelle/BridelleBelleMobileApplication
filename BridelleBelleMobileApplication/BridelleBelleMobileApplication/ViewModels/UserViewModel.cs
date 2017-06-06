using System;
using System.Collections.Generic;
using System.Text;

namespace BridelleBelleMobileApplication.ViewModels
{
	public class UserViewModel
	{
		public string Username;
		public IEnumerable<MagazineViewModel> OwnedMagazines;
	}
}
