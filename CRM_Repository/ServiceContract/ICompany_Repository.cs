using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
namespace CRM_Repository.ServiceContract
{
    public interface ICompany_Repository : IDisposable
    {
        void AddComapny(CompanyMaster companyobj);
        void UpdateCompany(CompanyMaster companyobj);
        void DeleteComapny(int id);
        CompanyMaster GetComapnybyid(int id);
        IQueryable<CompanyMaster> GetAllCompay();
        bool CheckComapnyExist(CompanyMaster CompObj, bool isUpdate);
    }
}
