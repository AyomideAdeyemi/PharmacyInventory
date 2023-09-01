using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class GenericNameRepository : RepositoryBase<GenericName>, IGenericNameRepository

    {
        private readonly DbSet<GenericName> _genericName;
        public GenericNameRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            _genericName = repositoryContext.Set<GenericName>();
        }


        public async Task<GenericName> GetGenericNameById(int id)
        {
            return await _genericName.FindAsync(id);
        }


        public async Task<PagedList<GenericName>> GetAllGenericName(GenericNameRequestInputParameter parameter)
        {
            var result = await _genericName.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _genericName.CountAsync();
            return new PagedList<GenericName>(result, count, parameter.PageNumber, parameter.PageSize);

        }
    }
}