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
    public class DeliveryTerms_Repository : IDeliveryTerms_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public DeliveryTerms_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddDeliveryTerms(DeliveryTermsMaster obj)
        {
            try
            {
                context.DeliveryTermsMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateDeliveryTerms(DeliveryTermsMaster obj)
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

        public void DeleteDeliveryTerms(int id)
        {
            try
            {
                DeliveryTermsMaster DelTerms = context.DeliveryTermsMasters.Find(id);
                if (DelTerms != null)
                {
                    context.DeliveryTermsMasters.Remove(DelTerms);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public DeliveryTermsMaster GetDeliveryTermsByID(int id)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TermsId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM DeliveryTermsMaster with(nolock) WHERE TermsId=@TermsId ", para).ConvertToList<DeliveryTermsMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<DeliveryTermsMaster> GetAllDeliveryTerms()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM DeliveryTermsMaster with(nolock) WHERE  IsActive = 1").ConvertToList<DeliveryTermsMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<DeliveryTermsMaster> DuplicateDeliveryTerms(string DeliveryTermName)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@DeliveryTermName", DeliveryTermName);
                return new dalc().GetDataTable_Text("SELECT * FROM DeliveryTermsMaster with(nolock) WHERE RTRIM(LTRIM(DeliveryName))=RTRIM(LTRIM(@DeliveryTermName))  AND IsActive = 1", para).ConvertToList<DeliveryTermsMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<DeliveryTermsMaster> DuplicateEditDeliveryTerms(int DeliveryTermId, string DeliveryTermName)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@DeliveryTermName", DeliveryTermName);
                para[1] = new SqlParameter().CreateParameter("@TermsId", DeliveryTermId);
                return new dalc().GetDataTable_Text("SELECT * FROM DeliveryTermsMaster with(nolock) WHERE RTRIM(LTRIM(DeliveryName))=RTRIM(LTRIM(@DeliveryTermName))  AND TermsId<>@TermsId AND IsActive = 1", para).ConvertToList<DeliveryTermsMaster>().AsQueryable();

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
