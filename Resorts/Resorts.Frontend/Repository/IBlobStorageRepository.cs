namespace Resorts.Frontend.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlobStorageRepository
    {
        Task<Uri> InsertResortDocument(byte[] docBytes, string blobName);
        Task<List<string>> ReadResortDocuments(List<string> blobUrls);
    }
}