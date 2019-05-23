using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class SMSSpeech_Repository : ISMSSpeech_Repository, IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;
        public SMSSpeech_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddSMSSpeech(SMSSpeechMaster obj)
        {
            try
            {
                context.SMSSpeechMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public SMSSpeechMaster GetSMSSpeechById(int SMSId)
        {
            try
            {
               // return context.SMSSpeechMasters.Find(id);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SMSId", SMSId);
                return new dalc().GetDataTable_Text("SELECT SM.SMSId,DM.DepartmentId,DM.DepartmentName,SM.SMSTitle,SM.SMS,SM.IsActive FROM SMSSpeechMaster As SM inner join departmentmaster as DM on DM.DepartmentId=SM.DepartmentId WHERE SMSId=@SMSId", para).ConvertToList<SMSSpeechMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<SMSSpeechMaster> DuplicateEditSMSSpeech(int SMSId, string SMSTitle)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SMSSpeech = context.SMSSpeechMasters.Where(x => x.SMSId != SMSId && x.SMSTitle == SMSTitle && x.IsActive == true);
                //    scope.Complete();
                //    return SMSSpeech.AsQueryable();
                //}


                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SMSId", SMSId);
                para[1] = new SqlParameter().CreateParameter("@SMSTitle", SMSTitle);
                return new dalc().GetDataTable_Text("SELECT * FROM SMSSpeechMaster with(nolock) WHERE RTRIM(LTRIM(SMSTitle))=RTRIM(LTRIM(@SMSTitle))  AND SMSId<>@SMSId AND IsActive = 1", para).ConvertToList<SMSSpeechMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<SMSSpeechMaster> DuplicateSMSSpeech(string SMSTitle)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SMSSpeech = context.SMSSpeechMasters.Where(x => x.SMSTitle == SMSTitle && x.IsActive == true);
                //    scope.Complete();
                //    return SMSSpeech.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SMSTitle", SMSTitle);
                return new dalc().GetDataTable_Text("SELECT * FROM SMSSpeechMaster with(nolock) WHERE RTRIM(LTRIM(SMSTitle))=RTRIM(LTRIM(@SMSTitle))", para).ConvertToList<SMSSpeechMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateSMSSpeech(SMSSpeechMaster obj)
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

        public IQueryable<SMSSpeechMaster> GetAllSMSSpeech()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SMSSpeech = context.SMSSpeechMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return SMSSpeech.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM SMSSpeechMaster with(nolock) where  IsActive=1").ConvertToList<SMSSpeechMaster>().AsQueryable();
            }
            catch
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
