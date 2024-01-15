using System;
using System.Globalization;
using Line.Data;
using Line.Models;
using Line.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Line.Repositories.Implementations
{
	public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly LineDbContext _dbContext;

        public DocumentRepository(LineDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Document>> GetDocumentsByDateRange(DateTime fromDate, DateTime untilDate, int customerId)
        {
            var documents = await GetNonDeletedDocumentsByUserId(customerId);

            return documents
                .Where(d => d.DocumentDateTime >= fromDate && d.DocumentDateTime <= untilDate)
                .OrderBy(d => d.DocumentDateTime)
                .ToList();
        }

        public async Task DeleteDocuments(string documentType, int UserId)
        {
            var documentsToDelete = _dbContext.Documents
            .Where(d => d.DocumentType == documentType && d.UserId == UserId)
            .ToList();

            foreach (var document in documentsToDelete)
            {
                _dbContext.Documents.Remove(document);
            }
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Document>> GetAllNoneDeletedDocuments()
        //{
        //    return await _dbContext.Documents
        //        .Where(d => !d.Deleted)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Document>> GetNonDeletedDocumentsByUserId(int UserId)
        {
            var documents = await _dbContext.Documents
                .Where(d => d.UserId == UserId && d.Deleted == false)
                .ToListAsync();
            return documents;
        }

        public async Task<IEnumerable<Document>> GetAllDeletedDocuments()
        {
            return await _dbContext.Documents
                .Where(d => d.Deleted)
                .ToListAsync();
        }



        //public IEnumerable<Document> GetDocumentsByDateRange(DateTime fromDate, DateTime toDate)
        //{
        //    var fromDateString = fromDate.ToString("yyyy-MM-ddTHH:mm:ss");
        //    var toDateString = toDate.ToString("yyyy-MM-ddTHH:mm:ss");

        //    return _dbContext.Documents
        //        .AsEnumerable()
        //        .Where(d => IsDocumentDateTimeInRange(d.DocumentDateTime, fromDateString, toDateString))
        //        .ToList();
        //}

        //private static bool IsDocumentDateTimeInRange(string documentDateTimeString, string fromDateTimeString, string toDateTimeString)
        //{
        //    DateTime documentDateTime;
        //    if (DateTime.TryParseExact(documentDateTimeString, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out documentDateTime))
        //    {
        //        var fromDateTime = DateTime.ParseExact(fromDateTimeString, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        //        var toDateTime = DateTime.ParseExact(toDateTimeString, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

        //        return documentDateTime >= fromDateTime && documentDateTime <= toDateTime;
        //    }
        //    return false;
        //}

        //public IEnumerable<Document> GetDocumentsByDateRange(DateTime fromDate, DateTime toDate)
        //{
        //    return _dbContext.Documents
        //        .Where(d =>
        //        {
        //            if (DateTime.TryParseExact(d.DocumentDateTime, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime documentDateTime))
        //            {
        //                return documentDateTime >= fromDate && documentDateTime <= toDate;
        //            }
        //            return false;
        //        })
        //        .ToList();
        //}

    }
}

