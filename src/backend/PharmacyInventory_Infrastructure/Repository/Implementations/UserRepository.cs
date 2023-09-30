using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext repositoryContext, UserManager<User> userManager) : base(repositoryContext)
        {
            _user = repositoryContext.Set<User>();
            _userManager = userManager;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _user.FindAsync(id);
        }


        public async Task<PagedList<User>> GetAllUsers(UserRequestInputParameter parameter)
        {
            var result =  _userManager.Users.OrderBy(x => x.FirstName);
            return await PagedList<User>.GetPagination(result, parameter.PageNumber, parameter.PageSize);
           
        }
    }
}
