using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IAccountType_Repository:IDisposable
    {
        void SaveAccountType(AccountTypeMaster objAccountType);
        void UpdateAccountType(AccountTypeMaster objAccountType);
        void DeleteAccountType(int AccountTypeId);
        AccountTypeMaster GetByAccountTypeId(int AccountTypeId);
        IQueryable<AccountTypeMaster> GetAccountType();
        bool IsExist(int AccountTypeId, string AccountType);
    }
}
