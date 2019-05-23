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
   public class FinancialYear_Repository:IFinancialYear_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public FinancialYear_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddFinancialYear(FinancialYearMaster obj)
        {
            try
            {
                context.FinancialYearMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void UpdateFinancialYear(FinancialYearMaster obj)
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
        public void DeleteFinancialYear(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@FinancialYearId",Id);
                FinancialYearMaster FinancialYear = new dalc().GetDataTable_Text("SELECT * FROM FinancialYearMaster with(nolock) WHERE FinancialYearId=@FinancialYearId", para).ConvertToList<FinancialYearMaster>().FirstOrDefault();
                if(FinancialYear!=null)
                {
                    context.FinancialYearMasters.Remove(FinancialYear);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public FinancialYearMaster GetFinancialYearByID(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@FinancialYearId", Id);
                var FinancialYear = new dalc().GetDataTable_Text("SELECT * FROM FinancialYearMaster with(nolock) WHERE FinancialYearId=@FinancialYearId", para).ConvertToList<FinancialYearMaster>().FirstOrDefault();
                return FinancialYear;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<FinancialYearMaster> GetAllFinancialYear()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var FinancialYear = new dalc().GetDataTable_Text("SELECT * FROM FinancialYearMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<FinancialYearMaster>().AsQueryable();
                return FinancialYear;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<FinancialYearMaster> DuplicateFinancialYear(string FinancialYear)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@FinancialYear", FinancialYear);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Financial = new dalc().GetDataTable_Text("SELECT * FROM FinancialYearMaster with(nolock) WHERE FinancialYear=@FinancialYear and IsActive=@IsActive", para).ConvertToList<FinancialYearMaster>().AsQueryable();
                return Financial.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<FinancialYearMaster> DuplicateEditFinancialYear(int FinancialYearId, string FinancialYear)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@FinancialYearId", FinancialYearId);
                para[1] = new SqlParameter().CreateParameter("@FinancialYear", FinancialYear);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Financial = new dalc().GetDataTable_Text("SELECT * FROM FinancialYearMaster with(nolock) WHERE FinancialYearId!=@FinancialYearId and FinancialYear=@FinancialYear and IsActive=@IsActive", para).ConvertToList<FinancialYearMaster>().AsQueryable();
                return Financial.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false;
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
