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
    public class Relation_Repository : IRelation_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Relation_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveRelation(RelationMaster objRelation)
        {
            try
            {
                context.RelationMasters.Add(objRelation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRelation(RelationMaster objRelation)
        {
            try
            {
                context.Entry(objRelation).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRelation(int RelationId)
        {
            try
            {
                RelationMaster objRelation = context.RelationMasters.Where(z => z.RelationId == RelationId).SingleOrDefault();
                objRelation.IsActive = false;
                context.Entry(objRelation).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RelationMaster GetByRelationId(int RelationId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.RelationMasters.Find(RelationId);
                //    scope.Complete();
                //    return data;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@RelationId", RelationId);
                return new dalc().GetDataTable_Text("SELECT * FROM RelationMaster with(nolock) WHERE RelationId=@RelationId", para).ConvertToList<RelationMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<RelationMaster> GetRelation()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.RelationMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return data;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM RelationMaster with(nolock) WHERE IsActive=1").ConvertToList<RelationMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(int RelationId, string RelationName)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.RelationMasters.Where(z => z.RelationId != RelationId && z.RelationName == RelationName && z.IsActive == true).Count();
                //    if (data > 0)
                //        return true;
                //    else
                //        return false;
                //}

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@RelationName", RelationName);
                para[1] = new SqlParameter().CreateParameter("@RelationId", RelationId);
                return new dalc().GetDataTable_Text("SELECT * FROM RelationMaster with(nolock) WHERE RTRIM(LTRIM(RelationName)) = RTRIM(LTRIM(@RelationName)) AND RelationId <> @RelationId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<UserReferenceRelationMaster> GetRelationById(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@relId", Id);
                return new dalc().GetDataTable_Text("SELECT * FROM UserReferenceRelationMaster with(nolock) WHERE RelationId=@relId", para).ConvertToList<UserReferenceRelationMaster>().AsQueryable();
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
