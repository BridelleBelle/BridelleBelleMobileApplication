using System;
using System.Net;
using System.Threading.Tasks;
using BelleBridal.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace BridalBelle.Database
{
	public class DocumentDbClient<T>:IDataPersistence<T> where T: class
	{
		protected static string EndPointUri = "https://bridalbelle.documents.azure.com:443/";
		protected static string AuthKey = "485YwqXC4WPxpAIPJp9coGwnkNk9jPlOx0kKYU0wheEQFidI3g5XZ9jc35YLgV9VDvaBhCpD1Q8dwIEjakaMiw==";

		private static DocumentClient Client;
		private static string DatabaseId = "bridalbelle";
		private string Collection = String.Empty;
		public DocumentDbClient(string collection)
		{
			this.Collection = collection;
			this.Initialize(Collection);
		}
		public async Task<Document> Create(T value)
		{
			return await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, Collection), value);
		}

		public async Task<T> Get(string id)
		{
			try
			{
				var uri = UriFactory.CreateDocumentUri(DatabaseId, Collection, id);
				var document = await Client.ReadDocumentAsync(uri);
				var x = (T) (dynamic) document;
				return x;
			}
			catch (DocumentClientException e)
			{
				throw e;
			}
		}

		public void Initialize(string collection)
		{
			Client = new DocumentClient(new Uri(EndPointUri),AuthKey,new ConnectionPolicy{EnableEndpointDiscovery = false});
			CreateDbIfNotExists().Wait();
			CreateCollectionIfNotExists().Wait();
		}

		private async Task CreateCollectionIfNotExists()
		{
			try
			{
				await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, Collection));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					await Client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DatabaseId),
						new DocumentCollection {Id = Collection}, new RequestOptions {OfferThroughput = 1000});
				}
				else
				{
					throw e;
				}
			}
		}
		private async Task CreateDbIfNotExists()
		{
			try
			{
				await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					await Client.CreateDatabaseAsync(new Microsoft.Azure.Documents.Database{Id = DatabaseId});
				}
			}
		}

		public async Task Remove(string id)
		{
			await Client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, Collection, id));
		}

		public async Task<Document> Update(string id, T value)
		{
			var uri = UriFactory.CreateDocumentUri(DatabaseId, Collection, id);
			var document = await Client.ReplaceDocumentAsync(uri,value);
			return document;
		}
	}
}
