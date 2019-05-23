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
   public class ApplicableCharge_Repository:IApplicableCharge_Repository
    {
        private elaunch_crmEntities context;
        public ApplicableCharge_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddAppliChar(ApplicableChargeMaster obj)
        {
            try
            {
                context.ApplicableChargeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public void UpdateAppliChar(ApplicableChargeMaster obj)
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
        public void DeleteAppllichar(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ApplicableChargeId", id);
                ApplicableChargeMaster ApplicableCharge = new dalc().GetDataTable_Text("SELECT * FROM ApplicableChargeMaster with(nolock) WHERE ApplicableChargeId=@ApplicableChargeId", para).ConvertToList<ApplicableChargeMaster>().FirstOrDefault();
                if (ApplicableCharge != null)
                {
                    context.ApplicableChargeMasters.Remove(ApplicableCharge);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
              throw  ex.InnerException;
            }
        }
        public ApplicableChargeMaster getApplicharbyId(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ApplicableChargeId", id);
                var ApplicableCharge = new dalc().GetDataTable_Text("SELECT * FROM ApplicableChargeMaster with(nolock) WHERE ApplicableChargeId=@ApplicableChargeId", para).ConvertToList<ApplicableChargeMaster>().FirstOrDefault();
                return ApplicableCharge;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ApplicableChargeMaster> GetAllApplichar()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ApplicableCharge = new dalc().GetDataTable_Text("SELECT * FROM ApplicableChargeMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<ApplicableChargeMaster>().AsQueryable();
                return ApplicableCharge;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ApplicableChargeMaster> DuplicateApplicableChargeName(string ApplicableChargeName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ApplicableChargeName", ApplicableChargeName);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ApplicableCharge = new dalc().GetDataTable_Text("SELECT * FROM ApplicableChargeMaster with(nolock) WHERE ApplicableChargeName=@ApplicableChargeName and IsActive=@IsActive", para).ConvertToList<ApplicableChargeMaster>().AsQueryable();
                return ApplicableCharge.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ApplicableChargeMaster> DuplicateEditApplicableChargeName(int ApplicableChargeId, string ApplicableChargeName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@ApplicableChargeId", ApplicableChargeId);
                para[1] = new SqlParameter().CreateParameter("@ApplicableChargeName", ApplicableChargeName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AppChar = new dalc().GetDataTable_Text("SELECT * FROM ApplicableChargeMaster with(nolock) WHERE ApplicableChargeId!=@ApplicableChargeId and ApplicableChargeName=@ApplicableChargeName and IsActive=@IsActive", para).ConvertToList<ApplicableChargeMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return AppChar.AsQueryable();
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
