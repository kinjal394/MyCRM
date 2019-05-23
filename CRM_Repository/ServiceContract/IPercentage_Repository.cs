using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IPercentage_Repository : IDisposable
    {
        void AddPercentage(PercentageMaster obj);
        void UpdatePercentage(PercentageMaster obj);
        PercentageMaster GetPercentageById(int id);
        IQueryable<PercentageMaster> GetAllPercentage();
        IQueryable<PercentageMaster> DuplicatePercentage(decimal Percentage);
        IQueryable<PercentageMaster> DuplicateEditPercentage(int PercentageId, decimal Percentage);
    }
}
