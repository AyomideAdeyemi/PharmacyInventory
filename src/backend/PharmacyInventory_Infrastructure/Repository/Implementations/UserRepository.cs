using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly DbSet<User> _user;

        public UserRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            _user = repositoryContext.Set<User>();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _user.FindAsync(id);
        }


        public async Task<PagedList<User>> GetAllUsers(UserRequestInputParameter parameter)
        {
            var result = await _user.Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _user.CountAsync();
            return new PagedList<User>(result, count, parameter.PageNumber, parameter.PageSize);

        }
    }
}
