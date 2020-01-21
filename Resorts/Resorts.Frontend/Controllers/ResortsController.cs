namespace Resorts.Frontend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repository;
    using Utils;

    public class ResortsController : Controller
    {
        private readonly IBlobStorageRepository _blobStorageRepository;
        private readonly IDocumentDbRepository<ResortInfo> _documentRepository;

        public ResortsController(IDocumentDbRepository<ResortInfo> documentRepository,
            IBlobStorageRepository blobStorageRepository)
        {
            _documentRepository = documentRepository;
            _blobStorageRepository = blobStorageRepository;
        }

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            var items = await _documentRepository.GetDocumentsAsync();
            foreach (var resortInfo in items)
            {
                var images = await GetImageFromDocument(resortInfo.AltLink);
                resortInfo.Images = images;
            }

            return View(items);
        }

        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync([Bind("ResortName,Description")] ResortInfo resortInfo,
            List<IFormFile> files)
        {
            if (!ModelState.IsValid) return View(resortInfo);
            resortInfo.ResortId = Guid.NewGuid().ToString();

            var lstBlobDetails = await CreateOrUpdateAnAttachment(files);
            await _documentRepository.CreateDocumentAsync(resortInfo, lstBlobDetails);

            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(string id)
        {
            if (id == null) return BadRequest();

            var resort = await _documentRepository.GetDocumentAsync(id);
            if (resort == null) return NotFound();

            var images = await GetImageFromDocument(resort.AltLink);
            resort.Images = images;

            return View(resort);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("ResortId,ResortName,Description")]
            ResortInfo resortInfo,
            List<IFormFile> files)
        {
            if (!ModelState.IsValid) return View(resortInfo);
            var lstBlobDetails = await CreateOrUpdateAnAttachment(files);
            await _documentRepository.UpdateDocumentAsync(resortInfo.ResortId, resortInfo, lstBlobDetails);

            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var item = await _documentRepository.GetDocumentAsync(id);

            var attachments = await GetImageFromDocument(item.AltLink);
            item.Images = attachments;

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null) return BadRequest();

            var item = await _documentRepository.GetDocumentAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("ResortId")] string id)
        {
            await _documentRepository.DeleteDocumentAsync(id).ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        private async Task<List<string>> GetImageFromDocument(string altLink)
        {
            var imgData = new List<string>();
            var attachmentsUrl = string.Concat("/", altLink);

            var documentAttachments = await _documentRepository.GetDocumentAttachment(attachmentsUrl);
            var blobData = await _blobStorageRepository.ReadResortDocuments(documentAttachments);
            foreach (var data in blobData) imgData.Add($"data:image/png;base64,{data}");

            return imgData;
        }

        private async Task<List<BlobDetails>> CreateOrUpdateAnAttachment(List<IFormFile> files)
        {
            var lstBlobDetails = new List<BlobDetails>();
            if (files != null)
                foreach (var formFile in files)
                {
                    var stream = formFile.OpenReadStream();
                    var length = (int)formFile.Length;

                    var data = new byte[length];
                    await stream.ReadAsync(data, 0, length);

                    var blobUri = await _blobStorageRepository.InsertResortDocument(data, formFile.FileName);
                    var fileExtIndex = formFile.FileName.IndexOf('.');
                    var blobName = formFile.FileName.Substring(0, fileExtIndex);
                    var contentType = formFile.ContentType;

                    var attachmentDetails = new BlobDetails
                    {
                        BlobUri = blobUri,
                        ContentType = contentType,
                        Name = blobName
                    };

                    lstBlobDetails.Add(attachmentDetails);
                }

            return lstBlobDetails;
        }
    }
}