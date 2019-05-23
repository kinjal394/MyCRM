using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
   public interface ISalaryHead_Repository : IDisposable
    {
        void AddSalaryHead(SalaryHeadMaster obj);
        void UpdateSalaryHead(SalaryHeadMaster obj);
        void DeleteSalaryHead(int id);
        SalaryHeadMaster GetSalaryHeadById(int id);
        IQueryable<SalaryHeadMaster> GetAllSalaryHead();
        IQueryable<SalaryHeadMaster> DuplicateSalaryHead(string SalaryHeadName);
        IQueryable<SalaryHeadMaster> DuplicateEditSalaryHead(int SalaryHeadId, string SalaryHeadName);
    }
}
