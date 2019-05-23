using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IFinancialYear_Repository: IDisposable
    {
        void AddFinancialYear(FinancialYearMaster obj);
        void UpdateFinancialYear(FinancialYearMaster obj);
        void DeleteFinancialYear(int id);
        FinancialYearMaster GetFinancialYearByID(int id);
        IQueryable<FinancialYearMaster> GetAllFinancialYear();
        IQueryable<FinancialYearMaster> DuplicateFinancialYear(string FinancialYear);
        IQueryable<FinancialYearMaster> DuplicateEditFinancialYear(int FinancialYearId, string FinancialYear);
    }
}
