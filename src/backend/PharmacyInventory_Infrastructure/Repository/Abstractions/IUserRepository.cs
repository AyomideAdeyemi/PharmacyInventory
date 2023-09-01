using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Repository.Abstractions
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserById(int id);
        Task<PagedList<User>> GetAllUsers(UserRequestInputParameter parameter);
    }
}
