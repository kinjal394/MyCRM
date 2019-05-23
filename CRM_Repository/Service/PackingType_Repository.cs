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
    public class PackingType_Repository:IPackingType_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public PackingType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddPackingType(PackingTypeMaster obj)
        {
            try
            {
                context.PackingTypeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdatePackingType(PackingTypeMaster obj)
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
        public void DeletePackingType(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PackingTypeId", id);
                PackingTypeMaster PackingType = new dalc().GetDataTable_Text("SELECT * FROM PackingTypeMaster with(nolock) WHERE PackingTypeId=@PackingTypeId", para).ConvertToList<PackingTypeMaster>().FirstOrDefault();
                if (PackingType != null)
                {
                    context.PackingTypeMasters.Remove(PackingType);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public PackingTypeMaster GetPackingTypeByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PackingTypeId", id);
                var PackingType = new dalc().GetDataTable_Text("SELECT * FROM PackingTypeMaster with(nolock) WHERE PackingTypeId=@PackingTypeId", para).ConvertToList<PackingTypeMaster>().FirstOrDefault();
                return PackingType;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<PackingTypeMaster> GetAllPackingType()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var PackingType = new dalc().GetDataTable_Text("SELECT * FROM PackingTypeMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<PackingTypeMaster>().AsQueryable();
                return PackingType;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<PackingTypeMaster> DuplicatePackingType(string PackingType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@PackingType", PackingType);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Type = new dalc().GetDataTable_Text("SELECT * FROM PackingTypeMaster with(nolock) WHERE PackingType=@PackingType and IsActive=@IsActive", para).ConvertToList<PackingTypeMaster>().AsQueryable();
                return Type.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<PackingTypeMaster> DuplicateEditPackingType(int PackingTypeId, string PackingType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@PackingTypeId", PackingTypeId);
                para[1] = new SqlParameter().CreateParameter("@PackingType", PackingType);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Type = new dalc().GetDataTable_Text("SELECT * FROM PackingTypeMaster with(nolock) WHERE PackingTypeId!=@PackingTypeId and PackingType=@PackingType and IsActive=@IsActive", para).ConvertToList<PackingTypeMaster>().AsQueryable();
                return Type.AsQueryable();
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
