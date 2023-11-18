using CloudStorage.Contexts;
using CloudStorage.Entity;
using CloudStorage.Interfaces;

namespace CloudStorage.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly MsDatabaseContext _context;

        public DocumentRepository(MsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Document> CreateDocument(Document document)
        {
            var newDocumentEntry = await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();

            return newDocumentEntry.Entity;
        }
    }
}