using Microsoft.AspNetCore.Identity;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Infrastructure.Repository.Implementations;

namespace PharmacyInventory_Infrastructure.UnitOfWorkManager
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _user;
        private readonly Lazy<IDrugRepository> _drugRepository;
        private readonly Lazy<ISupplierRepository> _supplierRepository;
        private readonly Lazy<IBrandRepository> _brandRepository;
        private readonly Lazy<IUnitRepository> _unitRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IGenericNameRepository> _genericNameRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext, UserManager<User> user)
        {
            _context = applicationDbContext;
           
            _drugRepository = new Lazy<IDrugRepository>(() => new DrugRepository(applicationDbContext));
            _supplierRepository = new Lazy<ISupplierRepository>(() => new SupplierRepository(applicationDbContext));
            _brandRepository = new Lazy<IBrandRepository>(() => new BrandRepository(applicationDbContext));
            _unitRepository = new Lazy<IUnitRepository>(() => new UnitRepository(applicationDbContext));
            _genericNameRepository = new Lazy<IGenericNameRepository>(() => new GenericNameRepository(applicationDbContext));
           _userRepository = new Lazy<IUserRepository>(() => new UserRepository(applicationDbContext, user));
        }
        public IDrugRepository Drug => _drugRepository.Value;
        public ISupplierRepository Supplier => _supplierRepository.Value;
        public IGenericNameRepository GenericName => _genericNameRepository.Value;
        public IBrandRepository Brand => _brandRepository.Value;
        public IUnitRepository Unit => _unitRepository.Value;
        public IUserRepository User => _userRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
