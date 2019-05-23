using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM_Repository.Service
{
    public class AccountType_Repository : IAccountType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public AccountType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveAccountType(AccountTypeMaster objAccountType)
        {
            try
            {
                context.AccountTypeMasters.Add(objAccountType);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAccountType(AccountTypeMaster objAccountType)
        {
            try
            {
                context.Entry(objAccountType).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAccountType(int AccountTypeId)
        {
            try
            {
                AccountTypeMaster objAccountType = context.AccountTypeMasters.Where(z => z.AccountTypeId == AccountTypeId).SingleOrDefault();
                objAccountType.IsActive = false;
                context.Entry(objAccountType).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AccountTypeMaster GetByAccountTypeId(int AccountTypeId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AccountTypeId", AccountTypeId);
                return new dalc().GetDataTable_Text("SELECT * FROM AccountTypeMaster with(nolock) WHERE AccountTypeId=@AccountTypeId AND IsActive=1", para).ConvertToList<AccountTypeMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<AccountTypeMaster> GetAccountType()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM AccountTypeMaster with(nolock)").ConvertToList<AccountTypeMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(int AccountTypeId, string AccountType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@AccountTypeId", AccountTypeId);
                para[1] = new SqlParameter().CreateParameter("@AccountType", AccountType);
                return new dalc().GetDataTable_Text("SELECT * FROM AccountTypeMaster with(nolock) WHERE AccountTypeId<>@AccountTypeId AND AccountType=@AccountType AND IsActive=1", para).Rows.Count > 0 ? true : false;
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
