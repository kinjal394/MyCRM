using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IAccountEntry_Repository: IDisposable
    {
        void InsertAccountEntry(AssetsExpenseMaster objaccount);
        void UpdateAccountEntry(AssetsExpenseMaster objaccount);
        IQueryable<AssetsExpenseMaster> GetAllAccountEntry();
        AssetsExpenseMaster GetAccountEntryByID(int AccountId);
        
    }
}
