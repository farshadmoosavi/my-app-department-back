using System;
using Line.Models;
using Line.Repositories.Implementations;
using Line.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Line.ModelView;
using System.Globalization;
using Castle.Core.Resource;

namespace Line.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocumnt(CreateDocumentDto documentDto)
        {

            try
            {
                // Create the new document using the data from the DTO
                var document = new Document
                {

                    DocumentType = documentDto.DocumentType,
                    UserName = documentDto.UserName,
                    DocumentDateTime = documentDto.DocumentDateTime,
                    DocumentDescription = documentDto.DocumentDescription,
                    Currency = documentDto.Currency,
                    DebtorValue = documentDto.DebtorValue,
                    CreditorValue = documentDto.CreditorValue,
                    BalanceValue = documentDto.BalanceValue
                };
                // Create the new document
                await _documentRepository.Create(document);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the document: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            try
            {
                var document = await _documentRepository.GetById(id);
                if (document == null)
                    return NotFound();

                return Ok(document);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the document: {ex.Message}");
            }
        }

        [HttpGet("Nondeletedbyuserid")]
        public async Task<IActionResult> GetNonDeletedDocumentsByCustomerId([FromQuery] int UserId)
        {
            var documents = await _documentRepository.GetNonDeletedDocumentsByUserId(UserId);
            var sortedDocuments = documents.OrderBy(doc => doc.DocumentDateTime).ToList(); // Change to OrderBy
            var documentData = sortedDocuments.Select(doc => new
            {
                DocumentId = doc.DocumentId,
                DocumentType = doc.DocumentType,
                DocumentDateTime = doc.DocumentDateTime,
                UserName = doc.UserName,
                Currency = doc.Currency,
                DocumentDescription = doc.DocumentDescription,
                DebtorValue = doc.DebtorValue,
                CreditorValue = doc.CreditorValue,
                BalanceValue = doc.BalanceValue
            }).ToList(); // Convert to a list to ensure it's a collection.
            return Ok(documentData);
        }



        [HttpGet("deleted")]
        public async Task<IEnumerable<Document>> GetAllDeletedDocuments()
        {
            // Retrieve all deleted documents
            return await _documentRepository.GetAllDeletedDocuments();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, Document document)
        {
            try
            {
                var existingDocument = await _documentRepository.GetById(id);
                if (existingDocument == null)
                    return NotFound();

                existingDocument.DocumentType = document.DocumentType;
                existingDocument.DocumentDescription = document.DocumentDescription;

                // Update other properties as needed

                await _documentRepository.Update(existingDocument);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the document: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                await _documentRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the document: {ex.Message}");
            }
        }

        //[HttpDelete("{documentType}/{customerId}")]
        //public async Task<IActionResult> DeleteDocuments(string documentType, int customerId)
        //{
        //    try
        //    {
        //        // Assuming _documentRepository has a method to delete documents based on the conditions
        //        await _documentRepository.DeleteDocuments(documentType, customerId);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while deleting the documents: {ex.Message}");
        //    }
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteDocuments(string documentType, int customerId)
        {
            try
            {
                // Assuming _documentRepository has a method to delete documents based on the conditions
                await _documentRepository.DeleteDocuments(documentType, customerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the documents: {ex.Message}");
            }
        }

        [HttpGet("getnondeletedbydaterangeandcustomerid")]
        public async Task<IActionResult> GetDocumentsByDateRange(DateTime fromDate, DateTime untildate, int customerId)
        {
            try
            {
                //var documents = await _documentRepository.GetDocumentsByDateRange(fromDate, untildate, customerId);
                //return Ok(documents);
                var documents = await _documentRepository.GetDocumentsByDateRange(fromDate, untildate, customerId);
                var documentData = documents.Select(doc => new
                {
                    DocumentId = doc.DocumentId,
                    DocumentType = doc.DocumentType,
                    DocumentDateTime = doc.DocumentDateTime,
                    UserName = doc.UserName,
                    Currency = doc.Currency,
                    DocumentDescription = doc.DocumentDescription,
                    DebtorValue = doc.DebtorValue,
                    CreditorValue = doc.CreditorValue,
                    BalanceValue = doc.BalanceValue
                }).ToList(); // Convert to a list to ensure it's a collection.
                return Ok(documentData);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the documents: {ex.Message}");
            }

        }
    }
}
