using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BridelleBelleMobileApplication.Database
{
    public class BlobStorage
    {
        private CloudStorageAccount StorageAccount;
        private CloudBlobClient BlobClient;
        private CloudBlobContainer Container;

        private async Task Initilize()
        {
            StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=bellebridal;AccountKey=81DeI9P+orNd4OYtbD1G1yTivm5ZgfE6z/Df+2gdMc4tJnPQvfFZpLmL7WQilhVldNCy1C3oJRE4BLXCFtDRyA==;EndpointSuffix=core.windows.net");

            BlobClient = StorageAccount.CreateCloudBlobClient();
            Container = BlobClient.GetContainerReference("coverimages");
            await Container.CreateIfNotExistsAsync();
           await Container.SetPermissionsAsync(
                new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob});

            
        }

        public async Task<string> GetImage()
        {
            await Initilize();
            var result = Container.GetBlockBlobReference("testcover.jpeg");
            return await result.DownloadTextAsync();
        }
    }
}
