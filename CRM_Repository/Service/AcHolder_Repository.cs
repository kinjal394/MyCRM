using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
   public class AcHolder_Repository:IAcHolder_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public AcHolder_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddAcHolder(AcHolderMaster obj)
        {
            try
            {
                context.AcHolderMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateAcHolder(AcHolderMaster obj)
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
        public void DeleteAcHolder(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AcHolderCode", id);
                AcHolderMaster AcHolder = new dalc().GetDataTable_Text("SELECT * FROM AcHolderMaster with(nolock) WHERE AcHolderCode=@AcHolderCode", para).ConvertToList<AcHolderMaster>().FirstOrDefault();
                if (AcHolder != null)
                {
                    context.AcHolderMasters.Remove(AcHolder);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public AcHolderMaster GetAcHolderByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AcHolderCode", id);
                var AcHolder = new dalc().GetDataTable_Text("SELECT * FROM AcHolderMaster with(nolock) WHERE AcHolderCode=@AcHolderCode", para).ConvertToList<AcHolderMaster>().FirstOrDefault();
                return AcHolder;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<AcHolderMaster> GetAllAcHolder()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AcHolder = new dalc().GetDataTable_Text("SELECT * FROM AcHolderMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<AcHolderMaster>().AsQueryable();
                return AcHolder;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<AcHolderMaster> DuplicateAcHolder(string AcHolderName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@AcHolderName", AcHolderName);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AcHolder = new dalc().GetDataTable_Text("SELECT * FROM AcHolderMaster with(nolock) WHERE AcHolderName=@AcHolderName and IsActive=@IsActive", para).ConvertToList<AcHolderMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyType == AgencyTypeName && x.IsActive == true);
                return AcHolder.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<AcHolderMaster> DuplicateEditAcHolder(int AcHolderCode, string AcHolderName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@AcHolderCode", AcHolderCode);
                para[1] = new SqlParameter().CreateParameter("@AcHolderName", AcHolderName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AgencyType = new dalc().GetDataTable_Text("SELECT * FROM AcHolderMaster with(nolock) WHERE AcHolderCode!=@AcHolderCode and AcHolderName=@AcHolderName and IsActive=@IsActive", para).ConvertToList<AcHolderMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return AgencyType.AsQueryable();
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
