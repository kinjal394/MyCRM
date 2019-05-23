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
    public class SalesDocumentName_Repository:ISalesDocumentName_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public SalesDocumentName_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddSalesDocument(SalesDocumentNameMaster obj)
        {
            try
            {
                context.SalesDocumentNameMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void UpdateSalesDocument(SalesDocumentNameMaster obj)
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
        public void DeleteSalesDocument(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SalesDocId", Id);
                SalesDocumentNameMaster SalesDocument = new dalc().GetDataTable_Text("SELECT * FROM SalesDocumentNameMaster with(nolock) WHERE SalesDocId=@SalesDocId", para).ConvertToList<SalesDocumentNameMaster>().FirstOrDefault();
                if(SalesDocument!=null)
                {
                    context.SalesDocumentNameMasters.Remove(SalesDocument);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public SalesDocumentNameMaster GetSalesDocumentById(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SalesDocId", Id);
                var SalesDocument = new dalc().GetDataTable_Text("SELECT * FROM SalesDocumentNameMaster with(nolock) WHERE SalesDocId=@SalesDocId", para).ConvertToList<SalesDocumentNameMaster>().FirstOrDefault();
                return SalesDocument;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<SalesDocumentNameMaster> GetAllSalesDocument()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("IsActive", "true");
                var SalesDocument = new dalc().GetDataTable_Text("SELECT * FROM SalesDocumentNameMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<SalesDocumentNameMaster>().AsQueryable();
                return SalesDocument;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<SalesDocumentNameMaster> DuplicateSalesDocument(string SalesDocument)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SalesDocument", SalesDocument);
                para[1] = new SqlParameter().CreateParameter("IsActive", "true");
                var Sales = new dalc().GetDataTable_Text("SELECT * FROM SalesDocumentNameMaster with(nolock) WHERE SalesDocument=@SalesDocument and IsActive=@IsActive", para).ConvertToList<SalesDocumentNameMaster>().AsQueryable();
                return Sales.AsQueryable();

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<SalesDocumentNameMaster> DuplicateEditSalesDocument(int SalesDocId, string SalesDocument)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@SalesDocId", SalesDocId);
                para[1] = new SqlParameter().CreateParameter("@SalesDocument", SalesDocument);
                para[2] = new SqlParameter().CreateParameter("IsActive", "true");
                var Sales = new dalc().GetDataTable_Text("SELECT * FROM SalesDocumentNameMaster with(nolock) WHERE SalesDocId!=@SalesDocId and SalesDocument=@SalesDocument and IsActive=@IsActive", para).ConvertToList<SalesDocumentNameMaster>().AsQueryable();
                return Sales.AsQueryable();
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
