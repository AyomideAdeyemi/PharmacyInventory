using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        private readonly DbSet<Brand> _brand;

        public BrandRepository(ApplicationDbContext repositoryContext) : base (repositoryContext)
        {
            _brand = repositoryContext.Set<Brand>();
        }

        public async Task<Brand> GetBrandById(string id)
        {
            return await _brand.FindAsync(id);
        }


        public async Task<PagedList<Brand>> GetAllBrands()
        {
            var parameter = new BrandRequestInputParameter();
            var result = await _brand.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _brand.CountAsync();
            return new PagedList<Brand>(result, count, parameter.PageNumber, parameter.PageSize);

        }
    }
}
