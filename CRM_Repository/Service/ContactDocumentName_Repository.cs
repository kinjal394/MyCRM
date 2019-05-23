using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Service;


namespace CRM_Repository.Service
{
    public class ContactDocumentName_Repository : IContactDocumentName_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public ContactDocumentName_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;

        }
        public void AddContactDocumentName(ContactDocumentNameMaster obj)
        {
            try
            {
                context.ContactDocumentNameMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void UpdateContactDocumentName(ContactDocumentNameMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteContactDocumentName(int id)
        {
            try
            {
                ContactDocumentNameMaster obj = context.ContactDocumentNameMasters.Find(id);
                if (obj != null)
                {
                    context.ContactDocumentNameMasters.Remove(obj);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ContactDocumentNameMaster> getAllContactDocumentName()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ContactDocumentNameMaster with(nolock) WHERE IsActive=1").ConvertToList<ContactDocumentNameMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ContactDocumentNameMaster GetContactDocumentNameById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactDocId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM ContactDocumentNameMaster with(nolock) WHERE ContactDocId=@ContactDocId AND IsActive = 1", para).ConvertToList<ContactDocumentNameMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<ContactDocumentNameMaster> GetContactDocumentById(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactDocId", Id);
                return new dalc().GetDataTable_Text("SELECT * FROM ContactDocumentNameMaster with(nolock) WHERE ContactDocId=@ContactDocId AND IsActive = 1", para).ConvertToList<ContactDocumentNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<ContactDocumentNameMaster> DuplicateEditContactDocumentName(int ContactDocId, string ContactDocName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ContactDocId", ContactDocId);
                para[1] = new SqlParameter().CreateParameter("@ContactDocName", ContactDocName);
                return new dalc().GetDataTable_Text("SELECT * FROM ContactDocumentNameMaster with(nolock) WHERE RTRIM(LTRIM(ContactDocName))=RTRIM(LTRIM(@ContactDocName)) AND ContactDocId != @ContactDocId AND IsActive = 1", para).ConvertToList<ContactDocumentNameMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<ContactDocumentNameMaster> DuplicateContactDocumentName(string ContactDocName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactDocName", ContactDocName);
                return new dalc().GetDataTable_Text("SELECT * FROM ContactDocumentNameMaster with(nolock) WHERE RTRIM(LTRIM(ContactDocName))=RTRIM(LTRIM(@ContactDocName)) AND IsActive = 1", para).ConvertToList<ContactDocumentNameMaster>().AsQueryable();
            }
            catch (Exception) { throw; }


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
