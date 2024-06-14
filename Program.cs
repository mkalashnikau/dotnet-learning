using System.Reflection.Metadata;

namespace Oop
{
    class Program
    {
        static void Main(string[] args)
        {
            IDocumentService documentService = new DocumentService();
            Console.WriteLine("Please enter document number:");
            var documentNumber = Convert.ToInt32(Console.ReadLine());
            var documents = documentService.GetDocumentsByNumber(documentNumber);
            foreach (var document in documents)
            {
                Console.WriteLine("Document Type: " + document.Type);
                Console.WriteLine("Document Number: " + document.DocumentNumber);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}