using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace BridalBelle.Database
{
	public class DocumentDbClient
	{
		protected static string EndPointUri = "https://bridalbell.documents.azure.com:443/";
		protected static string AuthKey = "rkBADsfYHXw89hdsGQBVi09Ix5XvXax0xPz90zOiNOqiDtouFLS9xKLGNXc7UdKgDLLY7B1lpCnWt1HYg9VdIw==";

		private static DocumentClient Client;

		public DocumentDbClient()
		{
			Access().Wait();
		}
		private async Task Access()
		{
			try
			{
				Client = new DocumentClient(new Uri(EndPointUri), AuthKey);
			}
			catch(Exception e)
			{
				
			}
		}
	}
}
