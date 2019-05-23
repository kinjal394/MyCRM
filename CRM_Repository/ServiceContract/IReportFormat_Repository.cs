using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
   public interface IReportFormat_Repository: IDisposable
    {
        void AddReportFormat(ReportFormatMaster obj);
        void UpdateReportFormat(ReportFormatMaster obj);
        void DeleteReportFormat(int id);
        ReportFormatMaster GetReportFormatByID(int id);
        IQueryable<ReportFormatMaster> GetAllReportFormat();
        IQueryable<ReportFormatMaster> DuplicateReportFormat(string CompanyCode);
        IQueryable<ReportFormatMaster> DuplicateEditReportFormat(int RotFormatId, string CompanyCode);
    }
}
