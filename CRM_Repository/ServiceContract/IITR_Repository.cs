using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IITR_Repository : IDisposable
    {
        void AddITR(ITRMaster objitr);
        void UpdateITR(ITRMaster objitr);
        void DeleteITR(int id);
        ITRMaster GetITRByID(int id);
        IQueryable<ITRMaster> GetAllITR();
        IQueryable<ITRMaster> DuplicateITR(string ITRName);
        IQueryable<ITRMaster> DuplicateEditITRName(int ITRId, string ITRName);
    }
}
