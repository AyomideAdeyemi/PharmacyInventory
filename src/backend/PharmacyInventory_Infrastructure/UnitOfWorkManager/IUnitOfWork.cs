using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyInventory_Infrastructure.Repository.Abstractions;

namespace PharmacyInventory_Infrastructure.UnitOfWorkManager
{
    public interface IUnitOfWork
    {
        IDrugRepository Drug { get; }

        void SaveAsync();
    }
}
