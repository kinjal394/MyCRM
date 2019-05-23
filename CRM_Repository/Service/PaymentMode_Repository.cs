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
   public class PaymentMode_Repository:IPaymentMode_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public PaymentMode_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddPaymentMode(PaymentModeMaster obj)
        {
            try
            {
                context.PaymentModeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void UpdatePaymentMode(PaymentModeMaster obj)
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
        public void DeletePaymentMode(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PaymentModeId", id);
                PaymentModeMaster PaymentMode= new dalc().GetDataTable_Text("SELECT * FROM PaymentModeMaster with(nolock) WHERE PaymentModeId=@PaymentModeId ", para).ConvertToList<PaymentModeMaster>().FirstOrDefault();
                if(PaymentMode!=null)
                {
                    context.PaymentModeMasters.Remove(PaymentMode);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public PaymentModeMaster GetPaymentModeByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("PaymentModeId", id);
                var PaymentMode = new dalc().GetDataTable_Text("SELECT * FROM PaymentModeMaster with(nolock) WHERE PaymentModeId=@PaymentModeId", para).ConvertToList<PaymentModeMaster>().FirstOrDefault();
                return PaymentMode;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<PaymentModeMaster> GetAllPaymentMode()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var PaymentMode = new dalc().GetDataTable_Text("SELECT * FROM PaymentModeMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<PaymentModeMaster>().AsQueryable();
                return PaymentMode;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<PaymentModeMaster> DuplicatePaymentMode(string PaymentMode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("PaymentMode", PaymentMode);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Payment = new dalc().GetDataTable_Text("SELECT * FROM PaymentModeMaster with(nolock) WHERE PaymentMode=@PaymentMode and IsActive=@IsActive", para).ConvertToList<PaymentModeMaster>().AsQueryable();
                return Payment.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public IQueryable<PaymentModeMaster> DuplicateEditPaymentMode(int PaymentModeId, string PaymentMode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@PaymentModeId", PaymentModeId);
                para[1] = new SqlParameter().CreateParameter("@PaymentMode", PaymentMode);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Payment = new dalc().GetDataTable_Text("SELECT * FROM PaymentModeMaster with(nolock) WHERE PaymentModeId!=@PaymentModeId and PaymentMode=@PaymentMode and IsActive=@IsActive", para).ConvertToList<PaymentModeMaster>().AsQueryable();
                return Payment.AsQueryable();
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
