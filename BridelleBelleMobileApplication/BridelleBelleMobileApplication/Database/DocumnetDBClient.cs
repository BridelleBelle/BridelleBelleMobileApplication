using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using BridelleBelleMobileApplication.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
namespace BridelleBelleMobileApplication.Database
{
	public class DocumnetDBClient
	{
		DocumentClient Client;

		public DocumnetDBClient()
		{
			Client = new DocumentClient(new Uri(Keys.DocumentDbEndPointUri), Keys.DocumentDbAuthKey);
		}

	    public async Task CreateDatabase()
		{
			try
			{
				await Client.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database
				{
					Id = "bellebridal"
				});
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		private async Task CreateDbIfNotExists()
		{
			try
			{
				await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri("bridalbelle"));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					await Client.CreateDatabaseAsync(new Microsoft.Azure.Documents.Database { Id = "bridalbelle" });
				}
			}
		}

		public async Task CreateDocumentCollection()
		{
			try
			{
				// Create collection with 400 RU/s
				var x = await Client.CreateDocumentCollectionIfNotExistsAsync(
					UriFactory.CreateDatabaseUri("bridalbelle"),
					new DocumentCollection
					{
						Id = "magazines"
					},
					new RequestOptions
					{
						OfferThroughput = 400
					});
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
		}

		public List<Magazine> GetLatest()
		{
			var magazines = new List<Magazine>();
			foreach (var m in Client.CreateDocumentQuery<Magazine>(UriFactory.CreateDocumentCollectionUri("bridalbelle","magazines"), "SELECT TOP 4 * FROM magazines"))
			{
				magazines.Add(m);
			}

			return magazines;
		}

		public async Task<Magazine> Get(string id)
		{
			var response = await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri("bridalbelle", "magazines", id));
			return (Magazine) (dynamic) response.Resource;
		}

		public Models.User GetUser(string username, string password)
		{
			string sql = "SELECT c.id, c.username, c.magazines from c where c.username = '" + username + "' AND c.password = '" + password + "'";
			var users = new List<Models.User>();
			foreach (var user in Client.CreateDocumentQuery<Models.User>(UriFactory.CreateDocumentCollectionUri("bellebridal", "users2"), sql))
			{
				users.Add(user);
			}

			return users[0];

		}
	}
}
