using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
   public class ReportFormat_Repository:IReportFormat_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ReportFormat_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddReportFormat(ReportFormatMaster obj)
        {
            try
            {
                context.ReportFormatMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateReportFormat(ReportFormatMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void DeleteReportFormat(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@RotFormatId", id);
                ReportFormatMaster ReportFormat = new dalc().GetDataTable_Text("SELECT * FROM ReportFormatMaster with(nolock) WHERE RotFormatId=@RotFormatId", para).ConvertToList<ReportFormatMaster>().FirstOrDefault();
                if (ReportFormat != null)
                {
                    context.ReportFormatMasters.Remove(ReportFormat);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public ReportFormatMaster GetReportFormatByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@RotFormatId", id);
                var ReportFormat = new dalc().GetDataTable_Text("SELECT * FROM ReportFormatMaster with(nolock) WHERE RotFormatId=@RotFormatId", para).ConvertToList<ReportFormatMaster>().FirstOrDefault();
                return ReportFormat;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ReportFormatMaster> GetAllReportFormat()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ReportFormat = new dalc().GetDataTable_Text("SELECT * FROM ReportFormatMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<ReportFormatMaster>().AsQueryable();
                return ReportFormat;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ReportFormatMaster> DuplicateReportFormat(string CompanyCode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@CompanyCode", CompanyCode);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ReportFormat = new dalc().GetDataTable_Text("SELECT * FROM ReportFormatMaster with(nolock) WHERE CompanyCode=@CompanyCode and IsActive=@IsActive", para).ConvertToList<ReportFormatMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyType == AgencyTypeName && x.IsActive == true);
                return ReportFormat.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ReportFormatMaster> DuplicateEditReportFormat(int RotFormatId, string CompanyCode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@RotFormatId", RotFormatId);
                para[1] = new SqlParameter().CreateParameter("@CompanyCode", CompanyCode);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ReportFormat = new dalc().GetDataTable_Text("SELECT * FROM ReportFormatMaster with(nolock) WHERE RotFormatId!=@RotFormatId and CompanyCode=@CompanyCode and IsActive=@IsActive", para).ConvertToList<ReportFormatMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return ReportFormat.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
