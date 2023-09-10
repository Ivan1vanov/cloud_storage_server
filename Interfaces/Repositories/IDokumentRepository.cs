using CloudStorage.Entity;

namespace CloudStorage.Interfaces
{
    public interface IDokumentRepository
    {
        Task<Dokument> CreateDokumnet(Dokument dokument);
    }
}
