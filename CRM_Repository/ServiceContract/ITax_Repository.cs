using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ITax_Repository : IDisposable
    {
        void AddTax(TaxMaster tax);
        void UpdateTax(TaxMaster tax);
        TaxMaster GetTaxById(int id);
        IQueryable<TaxMaster> GetAllTax();
        bool CheckTax(TaxMaster obj, bool isUpdate);
    }
}
