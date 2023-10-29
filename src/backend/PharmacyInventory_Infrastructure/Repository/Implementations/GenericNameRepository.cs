using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
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


        public async Task<GenericName> GetGenericNameById(string id)
        {
            return await _genericName.FindAsync(id);
        }


        public async Task<PagedList<GenericName>> GetAllGenericName(GenericNameRequestInputParameter parameter)
        {
            var result =  _genericName.OrderBy(x => x.Name);
            return await PagedList<GenericName>.GetPagination(result, parameter.PageNumber, parameter.PageSize);

        }
    }
}