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
   public class AskcustomerDetails_Repository:IAskcustomerDetails_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public AskcustomerDetails_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddAskCustomerDetail(AskCustomerDetail obj)
        {
            try
            {
                context.AskCustomerDetails.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateAskCustomerDetail(AskCustomerDetail obj)
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
        public void DeleteAskCustomerDetail(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AskCustId", id);
                AskCustomerDetail AskCust = new dalc().GetDataTable_Text("SELECT * FROM AskcustomerDetails with(nolock) WHERE AskCustId=@AskCustId", para).ConvertToList<AskCustomerDetail>().FirstOrDefault();
                if (AskCust != null)
                {
                    context.AskCustomerDetails.Remove(AskCust);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public AskCustomerDetail GetAskCustomerDetailByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AskCustId", id);
                var AskCust = new dalc().GetDataTable_Text("SELECT * FROM AskcustomerDetails with(nolock) WHERE AskCustId=@AskCustId", para).ConvertToList<AskCustomerDetail>().FirstOrDefault();
                return AskCust;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<AskCustomerDetail> GetAllAskCustomerDetail()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AskCust = new dalc().GetDataTable_Text("SELECT * FROM AskcustomerDetails with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<AskCustomerDetail>().AsQueryable();
                return AskCust;
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
