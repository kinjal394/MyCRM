using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class DocumentUpload_Repository : IDocumentUpload_Repository, IDisposable
    {
        dalc odal = new dalc();
        private elaunch_crmEntities context;
        public DocumentUpload_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddDocumentUpload(EmpDocumentMaster empdocobj)
        {
            try
            {
                context.EmpDocumentMasters.Add(empdocobj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateDocumentUpload(EmpDocumentMaster empdocobj)
        {
            try
            {
                context.Entry(empdocobj).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public EmpDocumentMaster GetDocUploadById(int EmpDocId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    EmpDocumentMaster empdoc = new EmpDocumentMaster();
                //    empdoc = context.EmpDocumentMasters.Find(EmpDocId);
                //    scope.Complete();
                //    return empdoc;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@EmpDocId", EmpDocId);
                return new dalc().GetDataTable_Text("SELECT * FROM EmpDocumentMaster with(nolock) WHERE EmpDocId=@EmpDocId ", para).ConvertToList<EmpDocumentMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }
        }
        public IQueryable<EmpDocumentMaster> GetdocUploadById(int EmpId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@EmpId", EmpId);
                    return odal.GetDataTable_Text(@"select um.username EmpName,dm.docname DocName,um.username EmpName,
                                                edm.photo Photo from EmpDocumentMaster edm 
                                                inner join DocumentNameMaster dm on dm.docid=edm.docid
                                                inner join UserMaster um on um.userid=edm.empid
                                                 where edm.EmpId =@EmpId",para).ConvertToList<EmpDocumentMaster>().AsQueryable();
                    //EmpDocumentMaster cmaster = new EmpDocumentMaster();
                    //var lstEmpDoc = context.EmpDocumentMasters.Where(x => x.EmpId == EmpId && x.IsActive == true).AsQueryable();
                    //scope.Complete();
                    //return lstEmpDoc;
               // }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<EmpDocumentMaster> GetAllUploadDoc()
        {
            try
            {
               
                return new dalc().selectbyquerydt("SELECT * FROM EmpDocumentMaster with(nolock) WHERE IsActive = 1").ConvertToList<EmpDocumentMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<EmpDocumentMaster> CheckForDuplicateDoc(int EmpId, int DocId)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@EmpId", EmpId);
                para[1] = new SqlParameter().CreateParameter("@DocId", DocId);
                return new dalc().GetDataTable_Text("SELECT * FROM EmpDocumentMaster with(nolock) WHERE EmpId=@EmpId  AND DocId=@DocId AND IsActive = 1", para).ConvertToList<EmpDocumentMaster>().AsQueryable();

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
