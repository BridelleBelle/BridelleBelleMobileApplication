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
		Uri collectionLink;

		protected static string EndPointUri = "https://bridalbelle.documents.azure.com:443/";
		protected static string AuthKey = "485YwqXC4WPxpAIPJp9coGwnkNk9jPlOx0kKYU0wheEQFidI3g5XZ9jc35YLgV9VDvaBhCpD1Q8dwIEjakaMiw==";

		public DocumnetDBClient()
		{
			Client = new DocumentClient(new Uri(EndPointUri),AuthKey);
			collectionLink = UriFactory.CreateDocumentCollectionUri("bridalbelle", "magazines");
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
				await Client.CreateDocumentCollectionIfNotExistsAsync(
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

		public async Task<List<Magazine>> GetMagazine()
		{
		    return null;
		}

		public async Task<Magazine> Get(string id)
		{
		    var response = await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri("bridalbelle", "magazines", "4"));
		    return (Magazine) (dynamic) response.Resource;
		}
	}
}
