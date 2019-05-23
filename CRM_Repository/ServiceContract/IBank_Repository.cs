using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IBank_Repository : IDisposable
    {
        void AddBank(BankMaster bankobj);
        void UpdateBank(BankMaster bankobj);
        void DeleteBank(int id);
        BankMaster GetBankById(int id);       
        IQueryable<BankMaster> getAllBank();
        bool CheckBankExist(BankMaster bankObj,bool isUpdate);
        BankMaster GetBank(int id);
    }
}
