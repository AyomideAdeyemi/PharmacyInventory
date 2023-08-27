using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Infrastructure.Repository.Implementations;

namespace PharmacyInventory_Infrastructure.UnitOfWorkManager
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IDrugRepository> _drugRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _drugRepository = new Lazy<IDrugRepository>(() => new DrugRepository(applicationDbContext));
        }
        public IDrugRepository Drug => _drugRepository.Value;

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
