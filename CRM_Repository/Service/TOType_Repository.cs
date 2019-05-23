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
   public  class TOType_Repository : ITOType_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public TOType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
           
        }
        public void AddTOType(TOTypeMaster obj)
        {
            try
            {
                context.TOTypeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void UpdateTOType(TOTypeMaster obj)
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
        public void DeleteTOType(int id)
        {
            try
            {
                TOTypeMaster Chat = context.TOTypeMasters.Find(id);
                if (Chat != null)
                {
                    context.TOTypeMasters.Remove(Chat);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TOTypeMaster> getAllTOType()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM TOTypeMaster with(nolock)").ConvertToList<TOTypeMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TOTypeMaster GetTOTypeById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TOTypeId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM TOTypeMaster with(nolock) WHERE TOTypeId=@TOTypeId AND IsActive = 1", para).ConvertToList<TOTypeMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<TOTypeMaster> GettOTypeById(int TOTypeId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TOTypeId", TOTypeId);
                return new dalc().GetDataTable_Text("SELECT * FROM TOTypeMaster with(nolock) WHERE TOTypeId=@TOTypeId AND IsActive = 1", para).ConvertToList<TOTypeMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<TOTypeMaster> DuplicateEditToTypeName(int TOTypeId, string TOType) {
            try {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TOTypeId", TOTypeId);
                para[1] = new SqlParameter().CreateParameter("@TOType", TOType);
                return new dalc().GetDataTable_Text("SELECT * FROM TOTypeMaster with(nolock) WHERE RTRIM(LTRIM(TOType))=RTRIM(LTRIM(@TOType)) AND TOTypeId != @TOTypeId AND IsActive = 1", para).ConvertToList<TOTypeMaster>().AsQueryable();
            }
            catch(Exception)
            {
                throw;
            }
        }
        public IQueryable<TOTypeMaster> DuplicateToType(string TOType) {
            try {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TOType", TOType);
                return new dalc().GetDataTable_Text("SELECT * FROM TOTypeMaster with(nolock) WHERE RTRIM(LTRIM(TOType))=RTRIM(LTRIM(@TOType)) AND IsActive = 1", para).ConvertToList<TOTypeMaster>().AsQueryable();
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
