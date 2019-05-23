using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Transactions;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class TermsAndCondition_Repository : ITermsAndCondition_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();
        public TermsAndCondition_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddTermsAndCondition(TermsAndConditionMaster obj)
        {
            try
            {
                odal.updatedata(@"insert into TermsAndConditionMaster (Title,IsActive,Description) values ('" + obj.Title + "','1',N'" + obj.Description.Trim().Replace("'", "''") + "')");
                //context.TermsAndConditionMasters.Add(obj);
                //context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public TermsAndConditionMaster GetTermsAndConditionById(int id)
        {
            try
            {
                //return context.TermsAndConditionMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TermsId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM TermsAndConditionMaster with(nolock) WHERE TermsId = @TermsId AND IsActive = 1", para).ConvertToList<TermsAndConditionMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<TermsAndConditionMaster> GetAllTermsAndCondition()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var TermsAndConditions = context.TermsAndConditionMasters;
                //    scope.Complete();
                //    return TermsAndConditions;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM TermsAndConditionMaster with(nolock) WHERE IsActive = 1").ConvertToList<TermsAndConditionMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TermsAndConditionMaster> DuplicateEditTermsAndCondition(int TermsId, string title)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var terms = context.TermsAndConditionMasters.Where(x => x.TermsId != TermsId && x.Title == title && x.IsActive == true);
                //    scope.Complete();
                //    return terms.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TermsId", TermsId);
                para[1] = new SqlParameter().CreateParameter("@title", title);
                return new dalc().GetDataTable_Text("SELECT * FROM TermsAndConditionMaster with(nolock) WHERE TermsId <> @TermsId AND title = @title AND IsActive = 1", para).ConvertToList<TermsAndConditionMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<TermsAndConditionMaster> DuplicateTermsAndCondition(string title)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var terms = context.TermsAndConditionMasters.Where(x => x.Title == title && x.IsActive == true);
                //    scope.Complete();
                //    return terms.AsQueryable();
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@title", title);
                return new dalc().GetDataTable_Text("SELECT * FROM TermsAndConditionMaster with(nolock) WHERE title = @title AND IsActive = 1", para).ConvertToList<TermsAndConditionMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateTermsAndCondition(TermsAndConditionMaster obj)
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
