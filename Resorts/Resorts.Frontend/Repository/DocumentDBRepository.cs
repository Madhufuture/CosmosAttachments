namespace Resorts.Frontend.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using Microsoft.Extensions.Configuration;
    using Utils;

    public class DocumentDbRepository<T> : IDocumentDbRepository<T> where T : class
    {
        private readonly IConfiguration _config;

        private DocumentClient _client;
        private string _collectionId;
        private string _databaseId;

        public DocumentDbRepository(IConfiguration config)
        {
            _config = config;

            Initialize();
        }


        public async Task<Document> CreateDocumentAsync(T item, List<BlobDetails> lstBlobDetailses)
        {
            try
            {
                var response =
                    await _client.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), item);
                if (response.StatusCode != HttpStatusCode.Created) return response;
                var attachmentUrl = string.Concat(response.Resource.AltLink, "/attachments/");

                foreach (var itm in lstBlobDetailses)
                {
                    var data = new
                    {
                        id = itm.Name,
                        media = itm.BlobUri,
                        contentType = itm.ContentType
                    };

                    await _client.CreateAttachmentAsync(attachmentUrl, data);
                }

                return response;
            }
            catch (DocumentClientException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task DeleteDocumentAsync(string id)
        {
            await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
        }

        public async Task<T> GetDocumentAsync(string id)
        {
            Document document =
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
            try
            {
                return (T) (dynamic) document;
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<List<string>> GetDocumentAttachment(string attachmentUrl)
        {
            var attachmentLinks = new List<string>();
            try
            {
                var attachments = await _client.ReadAttachmentFeedAsync(attachmentUrl);
                foreach (var attachment in attachments) attachmentLinks.Add(attachment.MediaLink);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return attachmentLinks;
        }

        public async Task<IEnumerable<T>> GetDocumentsAsync()
        {
            var results = new List<T>();
            try
            {
                var query = _client
                    .CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                        new FeedOptions {MaxItemCount = -1}).AsDocumentQuery();
                while (query.HasMoreResults) results.AddRange(await query.ExecuteNextAsync<T>());
            }
            catch (DocumentQueryException ex)
            {
                Console.WriteLine(ex.StatusCode);
            }

            return results;
        }

        public async Task<Document> UpdateDocumentAsync(string id, T item, List<BlobDetails> lstBlobDetails)
        {
            try
            {
                var updateResponse =
                    await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id),
                        item);
                if (updateResponse.StatusCode != HttpStatusCode.OK) return updateResponse;

                var attachmentsUrl = string.Concat(updateResponse.Resource.AltLink, "/attachments/");
                foreach (var lstBlobDetail in lstBlobDetails)
                {
                    var data = new
                    {
                        id = lstBlobDetail.Name,
                        media = lstBlobDetail.BlobUri,
                        contentType = lstBlobDetail.ContentType
                    };

                    await _client.UpsertAttachmentAsync(attachmentsUrl, data);
                }

                return updateResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private void Initialize()
        {
            var configuration = _config.GetSection("CosmosConfig");

            var cosmosEndpoint = configuration.GetValue<string>("AccountEndpoint");
            var cosmosKey = configuration.GetValue<string>("AccountKeys");

            _databaseId = configuration.GetValue<string>("Database");
            _collectionId = configuration.GetValue<string>("Collection");
            _client = new DocumentClient(new Uri(cosmosEndpoint), cosmosKey);

            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    await _client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseId),
                        new DocumentCollection {Id = _collectionId},
                        new RequestOptions {OfferThroughput = 1000});
                else
                    throw;
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    await _client.CreateDatabaseAsync(new Database {Id = _databaseId});
                else
                    throw;
            }
        }
    }
}