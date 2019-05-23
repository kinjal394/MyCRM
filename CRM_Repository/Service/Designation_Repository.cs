using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class Designation_Repository : IDesignation_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Designation_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddDesignation(DesignationMaster obj)
        {
            try
            {
                context.DesignationMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateDesignation(DesignationMaster obj)
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
        public void DeleteDesignation(int id)
        {
            try
            {
                DesignationMaster Designation = context.DesignationMasters.Find(id);
                if (Designation != null)
                {
                    context.DesignationMasters.Remove(Designation);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public DesignationMaster GetDesignationByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DesignationId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM DesignationMaster with(nolock) WHERE DesignationId=@DesignationId AND IsActive = 1", para).ConvertToList<DesignationMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<DesignationMaster> GetAllDesignation()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM DesignationMaster with(nolock) WHERE IsActive = 1").ConvertToList<DesignationMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<DesignationMaster> DuplicateDesignation(string DesignationName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DesignationName", DesignationName);
                return new dalc().GetDataTable_Text("SELECT * FROM DesignationMaster with(nolock) WHERE RTRIM(LTRIM(DesignationName))=RTRIM(LTRIM(@DesignationName))  AND IsActive = 1", para).ConvertToList<DesignationMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<DesignationMaster> DuplicateEditDesignation(int DesignationId, string DesignationName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@DesignationName", DesignationName);
                para[1] = new SqlParameter().CreateParameter("@DesignationId", DesignationId);
                return new dalc().GetDataTable_Text("SELECT * FROM DesignationMaster with(nolock) WHERE RTRIM(LTRIM(DesignationName))=RTRIM(LTRIM(@DesignationName))  AND DesignationId<>@DesignationId  AND IsActive = 1", para).ConvertToList<DesignationMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
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
