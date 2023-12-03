using CloudStorage.Contexts;
using CloudStorage.Entity;
using CloudStorage.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Document>> GetDocumentsByOwner(string ownerId)
        {
            return await _context.Documents
            .Where(doc => doc.OwnerId == new Guid(ownerId))
            .Include(d => d.AllowedUsers)
            .ToListAsync();
        }
    }
}