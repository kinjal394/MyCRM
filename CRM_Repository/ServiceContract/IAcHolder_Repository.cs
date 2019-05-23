using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface IAcHolder_Repository: IDisposable
    {
        void AddAcHolder(AcHolderMaster obj);
        void UpdateAcHolder(AcHolderMaster obj);
        void DeleteAcHolder(int id);
        AcHolderMaster GetAcHolderByID(int id);
        IQueryable<AcHolderMaster> GetAllAcHolder();
        IQueryable<AcHolderMaster> DuplicateAcHolder(string AcHolderName);
        IQueryable<AcHolderMaster> DuplicateEditAcHolder(int AcHolderCode, string AcHolderName);
    }
}
