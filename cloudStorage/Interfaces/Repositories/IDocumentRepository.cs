using CloudStorage.Entity;

namespace CloudStorage.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> CreateDocument(Document document);
        Task<List<Document>> GetDocumentsByOwner(string ownerId);

    }
}
