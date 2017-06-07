using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using BridelleBelleMobileApplication.Models;
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

		public async Task<Models.User> GetUser(string username, string password)
		{

			string sql = "SELECT c.id, c.username, c.password from c where c.username = '" + username + "'";
			var users = new List<Models.RegisteredUser>();
			foreach (var user in Client.CreateDocumentQuery<Models.RegisteredUser>(UriFactory.CreateDocumentCollectionUri("bellebridal", "users2"), sql))
			{
				users.Add(user);
			}

			if (users[0].Username == username && DecodeFromBase64(users[0].Password) == password)
			{
				try
				{
					var response = await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri("bellebridal", "users2", users[0].Id));
					return (Models.User)(dynamic)response.Resource;

				}
				catch (Exception ex)
				{
					return null;
				}

			}
			else
			{
				return null;
			}
		}

		public async Task AddUser(RegisteredUser user)
		{
			await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("bellebridal", "users2"), user);
		}

		private string DecodeFromBase64(string data) //decode passwords for when we need to match passwords
		{
			var encodeDataAsBytes = System.Convert.FromBase64String(data);
			return System.Text.ASCIIEncoding.ASCII.GetString(encodeDataAsBytes);
		}
	}
}
