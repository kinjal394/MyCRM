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
    public class Bank_Repository : IBank_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Bank_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddBank(BankMaster bankobj)
        {
            try
            {
                context.BankMasters.Add(bankobj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool CheckBankExist(BankMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@AccountNo", obj.AccountNo);
                    para[1] = new SqlParameter().CreateParameter("@BankId", obj.BankId);
                    return new dalc().GetDataTable_Text("SELECT * FROM BankMaster with(nolock) WHERE RTRIM(LTRIM(AccountNo)) = RTRIM(LTRIM(@AccountNo)) AND BankId != @BankId AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
                else
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@AccountNo", obj.AccountNo);
                    para[1] = new SqlParameter().CreateParameter("@BankId", obj.BankId);
                    return new dalc().GetDataTable_Text("SELECT * FROM BankMaster with(nolock) WHERE RTRIM(LTRIM(AccountNo)) = RTRIM(LTRIM(@AccountNo)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteBank(int id)
        {
            try
            {
                BankMaster bank = context.BankMasters.Find(id);
                if (bank != null)
                {
                    context.BankMasters.Remove(bank);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<BankMaster> getAllBank()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM BankMaster with(nolock) WHERE IsActive = 1").ConvertToList<BankMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public BankMaster GetBankById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BankMaster with(nolock) WHERE BankId=@BankId AND IsActive = 1", para).ConvertToList<BankMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public BankMaster GetBank(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BankId", id);
                return new dalc().GetDataTable_Text(@"SELECT bk.BankId,bk.BeneficiaryName,bk.BranchName,bk.AccountNo,bk.IFSCCode,bk.IsActive,bk.SwiftCode,bk.CRNNo,bk.AccountTypeId,at.AccountType,
                                                    bk.RegisterEmail,bk.RegisterMobile,bk.StatementPassword,bk.BankCustomerCareNo,bk.BankCustomerCareEmail,bk.Note,bk.BankNameId,BN.BankName,bk.MICRCode,
                                                    bk.NickName,AC.AcHolderName As AcNickName FROM BankMaster bk WITH(NOLOCK) 
                                                    LEFT JOIN AccountTypeMaster at WITH(NOLOCK)  ON bk.AccountTypeId = at.AccountTypeId
                                                    LEFT JOIN BankNameMaster BN WITH(NOLOCK)  ON bk.BankNameId = BN.BankId
                                                    LEFT JOIN AcHolderMaster AC WITH(NOLOCK)  ON bk.NickName = AC.AcHolderCode
                                                    WHERE bk.BankId = @BankId AND bk.IsActive = 1", para).ConvertToList<BankMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void UpdateBank(BankMaster bankobj)
        {
            try
            {
                context.Entry(bankobj).State = System.Data.Entity.EntityState.Modified;
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
