using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public class DrugRepository : RepositoryBase<Drug>, IDrugRepository
    {
        public DrugRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }

}
