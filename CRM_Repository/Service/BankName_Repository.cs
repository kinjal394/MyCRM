using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class BankName_Repository : IBankName_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public BankName_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddBankName(BankNameMaster obj)
        {
            try
            {
                context.BankNameMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void DeleteBankName(int id)
        {
            try
            {
                BankNameMaster Chat = context.BankNameMasters.Find(id);
                if (Chat != null)
                {
                    context.BankNameMasters.Remove(Chat);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<BankNameMaster> getAllBankName()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM BankNameMaster with(nolock)").ConvertToList<BankNameMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public BankNameMaster GetBankNameById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BankNameMaster with(nolock) WHERE BankId=@BankId AND IsActive = 1", para).ConvertToList<BankNameMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<BankNameMaster> GetbankNameById(int BankId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankId", BankId);
                return new dalc().GetDataTable_Text("SELECT * FROM BankNameMaster with(nolock) WHERE BankId=@BankId AND IsActive = 1", para).ConvertToList<BankNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<BankNameMaster> DuplicateEditBankName(int BankId, string BankName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@BankName", BankName);
                para[1] = new SqlParameter().CreateParameter("@BankId", BankId);
                return new dalc().GetDataTable_Text("SELECT * FROM BankNameMaster with(nolock) WHERE RTRIM(LTRIM(BankName))=RTRIM(LTRIM(@BankName))  AND BankId<>@BankId  AND IsActive = 1", para).ConvertToList<BankNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<BankNameMaster> DuplicateBankName(string BankName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankName", BankName);
                return new dalc().GetDataTable_Text("SELECT * FROM BankNameMaster with(nolock) WHERE RTRIM(LTRIM(BankName))=RTRIM(LTRIM(@BankName))  AND IsActive = 1", para).ConvertToList<BankNameMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }


        public void UpdateBankName(BankNameMaster obj)
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
