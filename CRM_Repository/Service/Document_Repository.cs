using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class Document_Repository : IDocument_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Document_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddDocument(DocumentNameMaster docobj)
        {
            try
            {
                context.DocumentNameMasters.Add(docobj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckDocName(DocumentNameMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    //var data = context.DocumentNameMasters.Where(e => e.DocId != obj.DocId && e.DocName.Trim() == obj.DocName.Trim() && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;

                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@DocName", obj.DocName);
                    para[1] = new SqlParameter().CreateParameter("@DocId", obj.DocId);
                    return new dalc().GetDataTable_Text("SELECT * FROM DocumentNameMaster with(nolock) WHERE RTRIM(LTRIM(DocName))=RTRIM(LTRIM(@DocName)) AND DocId<>@DocId AND IsActive=1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    //var data = context.DocumentNameMasters.Where(e => e.DocName.Trim() == obj.DocName.Trim() && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@DocName", obj.DocName);
                    return new dalc().GetDataTable_Text("SELECT * FROM DocumentNameMaster with(nolock) WHERE RTRIM(LTRIM(DocName))=RTRIM(LTRIM(@DocName)) AND IsActive=1", para).Rows.Count > 0 ? true : false;

                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<DocumentNameMaster> GetAllDoc()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var doc = context.DocumentNameMasters.Where(x => x.IsActive == true).AsQueryable();
                //    scope.Complete();
                //    return doc;
                //}

                return new dalc().selectbyquerydt("SELECT * FROM DocumentNameMaster with(nolock) WHERE IsActive = 1").ConvertToList<DocumentNameMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DocumentNameMaster GetDocById(int DocId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Doc = context.DocumentNameMasters.Find(id);
                //    scope.Complete();
                //    return Doc;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DocId", DocId);
                return new dalc().GetDataTable_Text("SELECT * FROM DocumentNameMaster with(nolock) WHERE DocId=@DocId AND IsActive = 1", para).ConvertToList<DocumentNameMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateDocument(DocumentNameMaster docobj)
        {
            try
            {
                context.Entry(docobj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
