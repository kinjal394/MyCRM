using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class TransactionType_Repository : ITransactionType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public TransactionType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddTransactionType(TransactionTypeMaster obj)
        {
            try
            {
                context.TransactionTypeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteTransactionType(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TranTypeId", id);
                TransactionTypeMaster TransactionType = new dalc().GetDataTable_Text("SELECT * FROM TransactionTypeMaster with(nolock) WHERE TranTypeId=@TranTypeId", para).ConvertToList<TransactionTypeMaster>().FirstOrDefault();
                if (TransactionType != null)
                {
                    context.TransactionTypeMasters.Remove(TransactionType);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        

        public IQueryable<TransactionTypeMaster> DuplicateTransactionType(string TranType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TranType", TranType);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Transaction = new dalc().GetDataTable_Text("SELECT * FROM TransactionTypeMaster with(nolock) WHERE TranType=@TranType and IsActive=@IsActive", para).ConvertToList<TransactionTypeMaster>().AsQueryable();
                return Transaction.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<TransactionTypeMaster> DuplicateTransactionType(int TranTypeId, string TranType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@TranTypeId", TranTypeId);
                para[1] = new SqlParameter().CreateParameter("@TranType", TranType);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Transaction = new dalc().GetDataTable_Text("SELECT * FROM TransactionTypeMaster with(nolock) WHERE TranTypeId!=@TranTypeId and TranType=@TranType and IsActive=@IsActive", para).ConvertToList<TransactionTypeMaster>().AsQueryable();
                return Transaction.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<TransactionTypeMaster> GetAllTransactionType()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var TransactionType = new dalc().GetDataTable_Text("SELECT * FROM TransactionTypeMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<TransactionTypeMaster>().AsQueryable();
                return TransactionType;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public TransactionTypeMaster GetTransactionTypeByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TranTypeId", id);
                var TransactionType = new dalc().GetDataTable_Text("SELECT * FROM TransactionTypeMaster with(nolock) WHERE TranTypeId=@TranTypeId", para).ConvertToList<TransactionTypeMaster>().FirstOrDefault();
                return TransactionType;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateTransactionType(TransactionTypeMaster obj)
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
