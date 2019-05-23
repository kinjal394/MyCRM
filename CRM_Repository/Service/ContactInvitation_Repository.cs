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
   public class ContactInvitation_Repository:IContactInvitation_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ContactInvitation_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddConInv(ContactInvitationMaster obj)
        {
            try
            {
                context.ContactInvitationMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateConInv(ContactInvitationMaster obj) 
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
        public void DeleteConInv(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AgencyTypeId", id);
                ContactInvitationMaster ConInv = new dalc().GetDataTable_Text("SELECT * FROM ContactInvitationMaster with(nolock) WHERE ContactInvitationId=@ContactInvitationId", para).ConvertToList<ContactInvitationMaster>().FirstOrDefault();
                if (ConInv != null)
                {
                    context.ContactInvitationMasters.Remove(ConInv);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public ContactInvitationMaster GetConInvByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactInvitationId", id);
                var ConInv = new dalc().GetDataTable_Text("SELECT * FROM ContactInvitationMaster with(nolock) WHERE ContactInvitationId=@ContactInvitationId", para).ConvertToList<ContactInvitationMaster>().FirstOrDefault();
                return ConInv;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ContactInvitationMaster> GetAllConInv()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var ConInv = new dalc().GetDataTable_Text("SELECT * FROM ContactInvitationMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<ContactInvitationMaster>().AsQueryable();
                return ConInv;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ContactInvitationMaster> DuplicateConInv(string ContactInvitation)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ContactInvitation", ContactInvitation);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var data = new dalc().GetDataTable_Text("SELECT * FROM ContactInvitationMaster with(nolock) WHERE ContactInvitation=@ContactInvitation and IsActive=@IsActive", para).ConvertToList<ContactInvitationMaster>().AsQueryable();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ContactInvitationMaster> DuplicateEditConInv(int ContactInvitationId, string ContactInvitation)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@ContactInvitationId", ContactInvitationId);
                para[1] = new SqlParameter().CreateParameter("@ContactInvitation", ContactInvitation);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var data = new dalc().GetDataTable_Text("SELECT * FROM ContactInvitationMaster with(nolock) WHERE ContactInvitationId!=@ContactInvitationId and ContactInvitation=@ContactInvitation and IsActive=@IsActive", para).ConvertToList<ContactInvitationMaster>().AsQueryable();
                //var AgencyType = context.AgencyTypeMasters.Where(x => x.AgencyTypeId != AgencyTypeId && x.AgencyType == AgencyTypeName && x.IsActive == true);
                return data.AsQueryable();
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
