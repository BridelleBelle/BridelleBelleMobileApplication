using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Types;
namespace BridelleBelleMobileApplication
{
	public class ImageClient
	{
		private CloudStorageAccount CloudStorageAccount;
		private CloudBlobClient CloudBlobClient;
		private CloudBlobContainer CloudBlobContainer;

		public ImageClient()
		{
			Init();
		}

		public void Init()
		{
			CloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=testbb;AccountKey=0YPm7Ug4b5xqavTLnjg9EsQDHeY0nVhmsjFZmk6klzuHqOXg+J0vn/nITt1h6xfDUCbvJQinwvxu/7HRP3R1tw==;EndpointSuffix=core.windows.net");
			CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
			CloudBlobContainer = CloudBlobClient.GetContainerReference("magazines");
		}

		public async Task<string> GetImages(ImageType imageType, string fileName)
		{
			CloudBlobContainer = CloudBlobClient.GetContainerReference(imageType.ToString().ToLower());
			var blob = CloudBlobContainer.GetBlockBlobReference(fileName);
			await blob.FetchAttributesAsync();
			var byteArray = new byte[blob.Properties.Length];
			await blob.DownloadToByteArrayAsync(byteArray, 0);
			return Convert.ToBase64String(byteArray);
		}

		public string GetImageUris(ImageType imageType, string fileName)
		{
			CloudBlobContainer = CloudBlobClient.GetContainerReference(imageType.ToString().ToLower());
			var blob = CloudBlobContainer.GetBlockBlobReference(fileName);
			return blob.Uri.AbsoluteUri;
        }
	}
}
