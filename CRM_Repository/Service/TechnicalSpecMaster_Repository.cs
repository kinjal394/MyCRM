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
    public class TechnicalSpecMaster_Repository : ITechnicalSpecMaster_Repository,IDisposable
    {

        private CRM_Repository.Data.elaunch_crmEntities context;

        public TechnicalSpecMaster_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveTechnicalSpec(TechnicalSpecMaster objTechnicalSpecMaster)
        {
            try
            {
                context.TechnicalSpecMasters.Add(objTechnicalSpecMaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateTechnicalSpec(TechnicalSpecMaster objTechnicalSpecMaster)
        {
            try
            {
                context.Entry(objTechnicalSpecMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTechnicalSpec(int SpecificationId)
        {
            try
            {
                TechnicalSpecMaster objTechnicalSpecMaster = context.TechnicalSpecMasters.Where(z => z.SpecificationId == SpecificationId).SingleOrDefault();
                objTechnicalSpecMaster.IsActive = false;
                context.Entry(objTechnicalSpecMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TechnicalSpecMaster GetBySpecificationId(int SpecificationId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.TechnicalSpecMasters.Find(SpecificationId);
                //    scope.Complete();
                //    return data;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SpecificationId", SpecificationId);
                return new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecMaster with(nolock) WHERE SpecificationId=@SpecificationId ", para).ConvertToList<TechnicalSpecMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TechnicalSpecMaster> GetTechnicalSpec()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.TechnicalSpecMasters;
                //    scope.Complete();
                //    return data;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM TechnicalSpecMaster with(nolock) ").ConvertToList<TechnicalSpecMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(int SpecificationId, string TechSpec)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.TechnicalSpecMasters.Where(z => z.SpecificationId != SpecificationId && z.TechSpec == TechSpec && z.IsActive == true).Count();
                //    if (data > 0)
                //        return true;
                //    else
                //        return false;
                //}

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SpecificationId", SpecificationId);
                para[1] = new SqlParameter().CreateParameter("@TechSpec", TechSpec);
                return new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecMaster with(nolock) WHERE SpecificationId<>@SpecificationId AND RTRIM(LTRIM(TechSpec)) = RTRIM(LTRIM(@TechSpec)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

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
