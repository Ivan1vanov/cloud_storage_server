using CloudStorage.Contexts;
using CloudStorage.Entity;
using CloudStorage.Interfaces;

namespace CloudStorage.Repositories
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly AppDbContext _context;

        public DokumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dokument> CreateDokumnet(Dokument dokument)
        {
            var newDokumentEntry = await _context.Dokuments.AddAsync(dokument);
            await _context.SaveChangesAsync();

            return newDokumentEntry.Entity;
        }
    }
}