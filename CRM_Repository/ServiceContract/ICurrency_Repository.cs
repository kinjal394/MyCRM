using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface ICurrency_Repository : IDisposable
    {
        void AddCurrency(CurrencyMaster Currency);
        void UpdateCurrency(CurrencyMaster Currency);
        CurrencyMaster GetCurrencyById(int id);
        IQueryable<CurrencyMaster> GetAllCurrency();
        bool CheckCurrencyType(CurrencyMaster obj, bool isUpdate);
    }
}
