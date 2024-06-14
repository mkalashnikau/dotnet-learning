using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oop
{
    public interface IDocumentService
    {
        List<Document> GetDocumentsByNumber(int documentNumber);
    }
}
