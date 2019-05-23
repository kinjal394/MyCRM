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
    public class TechnicalSpecHead_Repository : ITechnicalSpecHead_Repository , IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public TechnicalSpecHead_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddTechnicalSpecHead(TechnicalSpecHeadMaster objTechnicalSpecHead)
        {
            try
            {
                context.TechnicalSpecHeadMasters.Add(objTechnicalSpecHead);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateTechnicalSpecHead(TechnicalSpecHeadMaster objTechnicalSpecHead)
        {
            try
            {
                context.Entry(objTechnicalSpecHead).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTechnicalSpecHead(int TechHeadId)
        {
            try
            {
                TechnicalSpecHeadMaster objTechnicalSpecHead = context.TechnicalSpecHeadMasters.Where(z => z.TechHeadId == TechHeadId).SingleOrDefault();
                objTechnicalSpecHead.IsActive = false;
                context.Entry(objTechnicalSpecHead).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TechnicalSpecHeadMaster GetByTechHeadId(int TechHeadId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TechHeadId", TechHeadId);
                return new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecHeadMaster with(nolock) WHERE TechHeadId=@TechHeadId AND IsActive=1", para).ConvertToList<TechnicalSpecHeadMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TechnicalSpecHeadMaster> GetTechHead()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM TechnicalSpecHeadMaster with(nolock)").ConvertToList<TechnicalSpecHeadMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TechnicalSpecHeadMaster> DuplicateTechnicalSpecHead(string TechHead)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TechHead", TechHead);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var TechnicalSpecHead = new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecHeadMaster with(nolock) WHERE TechHead=@TechHead and IsActive=@IsActive", para).ConvertToList<TechnicalSpecHeadMaster>().AsQueryable();
                return TechnicalSpecHead.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<TechnicalSpecHeadMaster> DuplicateEdiTechnicalSpecHead(int TechHeadId, string TechHead)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@TechHeadId", TechHeadId);
                para[1] = new SqlParameter().CreateParameter("@TechHead", TechHead);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var TechnicalSpecHead = new dalc().GetDataTable_Text("SELECT * FROM TechnicalSpecHeadMaster with(nolock) WHERE TechHeadId!=@TechHeadId and TechHead=@TechHead and IsActive=@IsActive", para).ConvertToList<TechnicalSpecHeadMaster>().AsQueryable();
                return TechnicalSpecHead.AsQueryable();
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
