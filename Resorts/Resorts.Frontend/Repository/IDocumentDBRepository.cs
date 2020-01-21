namespace Resorts.Frontend.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Utils;

    public interface IDocumentDbRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetDocumentsAsync();
        Task<T> GetDocumentAsync(string id);
        Task<Document> CreateDocumentAsync(T item, List<BlobDetails> lstBlobDetailses);
        Task<Document> UpdateDocumentAsync(string id, T item, List<BlobDetails> lstBlobDetails);
        Task DeleteDocumentAsync(string id);
        Task<List<string>> GetDocumentAttachment(string attachmentUrl);
    }
}