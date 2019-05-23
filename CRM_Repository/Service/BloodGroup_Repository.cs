using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class BloodGroup_Repository : IBloodGroup_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public BloodGroup_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddBloodGroup(BloodGroupMaster bloodgroup)
        {
            try
            {
                context.BloodGroupMasters.Add(bloodgroup);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckBloodGroup(BloodGroupMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@BloodGroup", obj.BloodGroup);
                    para[1] = new SqlParameter().CreateParameter("@BloodGroupId", obj.BloodGroupId);
                    return new dalc().GetDataTable_Text("SELECT * FROM BloodGroupMaster with(nolock) WHERE LOWER(BloodGroup) = LOWER(@BloodGroup) AND BloodGroupId != @BloodGroupId AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@BloodGroup", obj.BloodGroup);
                    return new dalc().GetDataTable_Text("SELECT * FROM BloodGroupMaster with(nolock) WHERE LOWER(BloodGroup) = LOWER(@BloodGroup) AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<BloodGroupMaster> GetAllBloodGroup()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM BloodGroupMaster with(nolock)").ConvertToList<BloodGroupMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public BloodGroupMaster GetBloodGroupById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BloodGroupId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BloodGroupMaster with(nolock) WHERE BloodGroupId = @BloodGroupId AND IsActive = 1", para).ConvertToList<BloodGroupMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateBloodGroup(BloodGroupMaster bloodgroup)
        {
            try
            {
                context.Entry(bloodgroup).State = System.Data.Entity.EntityState.Modified;
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
