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
    public class Specification_Repository : ISpecification_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Specification_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddSpecification(TechnicalSpecMaster obj)
        {
            try
            {
                context.TechnicalSpecMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateSpecification(TechnicalSpecMaster obj)
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

        public void DeleteSpecification(int id)
        {
            try
            {
                TechnicalSpecMaster SpecificationType = context.TechnicalSpecMasters.Find(id);
                if (SpecificationType != null)
                {
                    context.TechnicalSpecMasters.Remove(SpecificationType);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public TechnicalSpecMaster GetSpecificationByID(int SpecificationId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Specification = context.TechnicalSpecMasters.Find(id);
                //    scope.Complete();
                //    return Specification;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SpecificationId", SpecificationId);
                return new dalc().GetDataTable_Text("SELECT TSM.SpecificationId,TSM.TechHeadId,TSM.TechSpec,TM.TechHead,TSM.IsActive FROM TechnicalSpecMaster As TSM join TechnicalSpecHeadMaster as TM on TM.TechHeadId=TsM.TechHeadId  WHERE SpecificationId=@SpecificationId ", para).ConvertToList<TechnicalSpecMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<TechnicalSpecMaster> GetAllSpecification()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Specification = context.TechnicalSpecMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return Specification;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM TechnicalSpecMaster with(nolock) WHERE  IsActive = 1").ConvertToList<TechnicalSpecMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TechnicalSpecMaster> DuplicateSpecification(string TechSpec)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
            //    var Specification = context.TechnicalSpecMasters.Where(x => x.TechSpec == TechSpec && x.IsActive == true);
            //    scope.Complete();
            //    return Specification.AsQueryable();
            //}
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TechSpec", TechSpec);
                return new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecMaster with(nolock) WHERE RTRIM(LTRIM(TechSpec))=RTRIM(LTRIM(@TechSpec))  AND IsActive = 1", para).ConvertToList<TechnicalSpecMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        public IQueryable<TechnicalSpecMaster> DuplicateEditSpecification(int SpecificationId, string TechSpec)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            //{
            //    var Specification = context.TechnicalSpecMasters.Where(x => x.SpecificationId != SpecificationId && x.TechSpec == TechSpec && x.IsActive == true);
            //    scope.Complete();
            //    return Specification.AsQueryable();
            //}
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@SpecificationId", SpecificationId);
                para[1] = new SqlParameter().CreateParameter("@TechSpec", TechSpec);
                return new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecMaster with(nolock) WHERE RTRIM(LTRIM(TechSpec))=RTRIM(LTRIM(@TechSpec))  AND SpecificationId<>@SpecificationId AND IsActive = 1", para).ConvertToList<TechnicalSpecMaster>().AsQueryable();
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
