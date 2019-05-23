using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IAgencyType_Repository : IDisposable
    {
        void AddAgencyType(AgencyTypeMaster obj);
        void UpdateAgencyType(AgencyTypeMaster obj);
        void DeleteAgencyType(int id);
        AgencyTypeMaster GetAgencyTypeByID(int id);
        IQueryable<AgencyTypeMaster> GetAllAgencyType();
        IQueryable<AgencyTypeMaster> DuplicateAgencyType(string AgencyType);
        IQueryable<AgencyTypeMaster> DuplicateEditAgencyType(int AgencyTypeId, string AgencyType);
    }
}
