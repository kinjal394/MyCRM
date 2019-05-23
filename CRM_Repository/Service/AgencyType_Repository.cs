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
    public class AgencyType_Repository : IAgencyType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public AgencyType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddAgencyType(AgencyTypeMaster obj)
        {
            try
            {
                context.AgencyTypeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateAgencyType(AgencyTypeMaster obj)
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

        public void DeleteAgencyType(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AgencyTypeId", id);
                AgencyTypeMaster AgencyType = new dalc().GetDataTable_Text("SELECT * FROM AgencyTypeMaster with(nolock) WHERE AgencyTypeId=@AgencyTypeId", para).ConvertToList<AgencyTypeMaster>().FirstOrDefault();
                if (AgencyType != null)
                {
                    context.AgencyTypeMasters.Remove(AgencyType);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public AgencyTypeMaster GetAgencyTypeByID(int id)
        {
            try
            {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@AgencyTypeId", id);
                    var AgencyType = new dalc().GetDataTable_Text("SELECT * FROM AgencyTypeMaster with(nolock) WHERE AgencyTypeId=@AgencyTypeId", para).ConvertToList<AgencyTypeMaster>().FirstOrDefault();
                    return AgencyType;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<AgencyTypeMaster> GetAllAgencyType()
        {
            try
            {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                    var AgencyType = new dalc().GetDataTable_Text("SELECT * FROM AgencyTypeMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<AgencyTypeMaster>().AsQueryable();
                    return AgencyType;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<AgencyTypeMaster> DuplicateAgencyType(string AgencyTypeName)
        {
            try
            {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@AgencyType", AgencyTypeName);
                    para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                    var AgencyType = new dalc().GetDataTable_Text("SELECT * FROM AgencyTypeMaster with(nolock) WHERE AgencyType=@AgencyType and IsActive=@IsActive", para).ConvertToList<AgencyTypeMaster>().AsQueryable();
                    //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyType == AgencyTypeName && x.IsActive == true);
                    return AgencyType.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<AgencyTypeMaster> DuplicateEditAgencyType(int AgencyTypeId, string AgencyTypeName)
        {
            try
            {
                    SqlParameter[] para = new SqlParameter[3];
                    para[0] = new SqlParameter().CreateParameter("@AgencyTypeId", AgencyTypeId);
                    para[1] = new SqlParameter().CreateParameter("@AgencyType", AgencyTypeName);
                    para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                    var AgencyType = new dalc().GetDataTable_Text("SELECT * FROM AgencyTypeMaster with(nolock) WHERE AgencyTypeId!=@AgencyTypeId and AgencyType=@AgencyType and IsActive=@IsActive", para).ConvertToList<AgencyTypeMaster>().AsQueryable();
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
