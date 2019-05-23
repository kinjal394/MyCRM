using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
    public class AddressType_Repository : IAddressType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public AddressType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddAddressType(AddressTypeMaster objAddressType)
        {
            try
            {
                context.AddressTypeMasters.Add(objAddressType);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteAddressType(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AddressTypeId", id);
                AddressTypeMaster AddressType = new dalc().GetDataTable_Text("SELECT * FROM AddressTypeMaster with(nolock) WHERE AddressTypeId=@AddressTypeId", para).ConvertToList<AddressTypeMaster>().FirstOrDefault();
                if (AddressType != null)
                {
                    context.AddressTypeMasters.Remove(AddressType);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }



        public IQueryable<AddressTypeMaster> DuplicateAddressType(string AddressTypeName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AddressTypeName", AddressTypeName);
                return new dalc().GetDataTable_Text("SELECT * FROM AddressTypeMaster with(nolock) WHERE RTRIM(LTRIM(AddressTypeName)) = RTRIM(LTRIM(@AddressTypeName)) AND IsActive=1 ", para).ConvertToList<AddressTypeMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<AddressTypeMaster> DuplicateEditAddressTypeName(int AddressTypeId, string AddressTypeName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@AddressTypeId", AddressTypeId);
                para[1] = new SqlParameter().CreateParameter("@AddressTypeName", AddressTypeName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var AddressType = new dalc().GetDataTable_Text("SELECT * FROM AddressTypeMaster with(nolock) WHERE AddressTypeId!=@AddressTypeId and AddressTypeName=@AddressTypeName and IsActive=@IsActive", para).ConvertToList<AddressTypeMaster>().AsQueryable();

                return AddressType.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public AddressTypeMaster GetAddressTypeByID(int AddressTypeId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AddressTypeId", AddressTypeId);
                return new dalc().GetDataTable_Text("SELECT * FROM AddressTypeMaster with(nolock) WHERE AddressTypeId=@AddressTypeId", para).ConvertToList<AddressTypeMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<AddressTypeMaster> GetAllAddressType()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM AddressTypeMaster with(nolock) WHERE IsActive=1").ConvertToList<AddressTypeMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateAddressType(AddressTypeMaster objAddressType)
        {
            try
            {
                context.Entry(objAddressType).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
