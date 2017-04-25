using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace BridalBelle.Database
{
	public class DocumentDBClient
	{
		static string EndPointUri = "https://bridalbell.documents.azure.com:443/";
		static string AuthKey = "rkBADsfYHXw89hdsGQBVi09Ix5XvXax0xPz90zOiNOqiDtouFLS9xKLGNXc7UdKgDLLY7B1lpCnWt1HYg9VdIw==";

		private static DocumentClient client;

		public void DoStuff()
		{
			MyMethod().Wait();
		}
		//read
		public static async Task MyMethod()
		{
			try
			{
				using (client = new DocumentClient(new Uri(EndPointUri), AuthKey))
				{
					string id = "advertisers";
					var db = (from d in client.CreateDatabaseQuery() where d.Id == id select d).AsEnumerable().FirstOrDefault();

					if (db == null)
					{
						db = await client.CreateDatabaseAsync(new Microsoft.Azure.Documents.Database() {Id = id});
						Console.WriteLine("Created database {0}", db.Id);
					}
					else
					{
						Console.WriteLine("Database {0} already exists.",db.Id);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
