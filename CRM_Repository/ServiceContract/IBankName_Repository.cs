using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
namespace CRM_Repository.ServiceContract
{
    public  interface IBankName_Repository : IDisposable
    {
        void AddBankName(BankNameMaster obj);
        void DeleteBankName(int id);
        IQueryable<BankNameMaster> getAllBankName();
        BankNameMaster GetBankNameById(int id);
        IQueryable<BankNameMaster> GetbankNameById(int BankId);
        IQueryable<BankNameMaster> DuplicateBankName(string BankName);
        IQueryable<BankNameMaster> DuplicateEditBankName(int BankId, string BankName);
        void UpdateBankName(BankNameMaster obj);

    }
}
