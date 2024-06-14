using Oop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oop
{
    public class DocumentService : IDocumentService
    {
        public List<Document> GetDocumentsByNumber(int documentNumber)
        {
            // Mock database
            List<Document> documents = new List<Document>()
        {
            new Patent
            {
                DocumentNumber = 1,
                Type = "Patent",
                Title = "Patent 1",
                Authors = new List<string> { "Author 1", "Author 2" },
                DatePublished = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1),
                UniqueId = 123
            },
            new Book
            {
                DocumentNumber = 2,
                Type = "Book",
                Title = "Book 1",
                Authors = new List<string> { "Author 1", "Author 2" },
                DatePublished = DateTime.Now,
                NumberOfPages = 200,
                Publisher = "Publisher 1",
                ISBN = "123-456"
            }
        };

            return documents.Where(d => d.DocumentNumber == documentNumber).ToList();
        }
    }
}
