namespace Resorts.Frontend.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly IConfiguration _config;
        private CloudBlobContainer _container;

        public BlobStorageRepository(IConfiguration config)
        {
            _config = config;

            InitializeBlobStorage();
        }

        public async Task<Uri> InsertResortDocument(byte[] docBytes, string blobName)
        {
            try
            {
                _container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, new BlobRequestOptions(),
                    new OperationContext()).Wait();
                var blobRef = _container.GetBlockBlobReference(blobName);
                await using (Stream data = new MemoryStream(docBytes))
                {
                    await blobRef.UploadFromStreamAsync(data);
                }

                return blobRef.Uri;
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<List<string>> ReadResortDocuments(List<string> blobUrls)
        {
            var lstImages = new List<string>();
            try
            {
                foreach (var blobUrl in blobUrls)
                {
                    var blobData = await _container.ServiceClient.GetBlobReferenceFromServerAsync(new Uri(blobUrl));
                    using (var ms = new MemoryStream())
                    {
                        await blobData.DownloadToStreamAsync(ms);
                        var arrBlobData = ms.ToArray();
                        lstImages.Add(Convert.ToBase64String(arrBlobData));
                    }
                }

                return lstImages;
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstImages;
        }

        private void InitializeBlobStorage()
        {
            var blobStorageConfig = _config.GetSection("BlobConfig");
            var storageConnectionString = blobStorageConfig.GetValue<string>("ConnectionString");
            var containerName = blobStorageConfig.GetValue<string>("Container");

            var storageAccountDetails = CloudStorageAccount.Parse(storageConnectionString);
            var client = storageAccountDetails.CreateCloudBlobClient();
            _container = client.GetContainerReference(containerName);
        }
    }
}