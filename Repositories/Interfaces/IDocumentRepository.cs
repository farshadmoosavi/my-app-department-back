using System;
using System.Xml.Linq;
using Line.Models;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;

namespace Line.Repositories.Interfaces
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByDateRange(DateTime fromDate, DateTime untildate, int userId);
        Task DeleteDocuments(string documentType, int userId);
        Task<IEnumerable<Document>> GetNonDeletedDocumentsByUserId(int customerId);
        Task<IEnumerable<Document>> GetAllDeletedDocuments();
    }
}

