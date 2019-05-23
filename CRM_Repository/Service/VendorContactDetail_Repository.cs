using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class VendorContactDetail_Repository : IVendorContactDetail_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public VendorContactDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddVendorContactDetail(VendorContactDetail obj)
        {
            try
            {
                context.VendorContactDetails.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateVendorContactDetail(VendorContactDetail obj)
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

        public void DeleteVendorContactDetail(int id)
        {
            try
            {
                VendorContactDetail VendorContact = context.VendorContactDetails.Find(id);
                if (VendorContact != null)
                {
                    context.VendorContactDetails.Remove(VendorContact);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public VendorContactDetail GetVendorContactDetailById(int Contactid)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var VendorContact = context.VendorContactDetails.Find(id);
                //    scope.Complete();
                //    return VendorContact;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Contactid", Contactid);
                 return new dalc().GetDataTable_Text("SELECT * FROM AttendanceMaster with(nolock) WHERE Contactid = @Contactid ", para).ConvertToList<VendorContactDetail>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VendorContactDetail> GetAllVendorContactDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var VendorContactDetails = context.VendorContactDetails.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return VendorContactDetails;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM VendorContactDetail with(nolock) where IsActive=1").ConvertToList<VendorContactDetail>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<vmVendorContactDetail> GetByVendorId(int VendorId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VendorId", VendorId);
                return odal.GetDataTable_Text(@"Select BC.*,D.DesignationName
                                            from VendorContactDetail As BC  with(nolock)
                                            Inner join DesignationMaster As D  with(nolock) on BC.DesignationId = D.DesignationId
                                            Where BC.VendorId =@VendorId  And ISNULL(BC.IsActive,0)=1",para).ConvertToList<vmVendorContactDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<VendorAddressDetail> GetAddressByVendorId(int id)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VendorId", id);
                return new dalc().GetDataTable_Text(@"Select BC.*,D.CityId,D.CityName,S.StateId,S.StateName,C.CountryId,C.CountryName from VendorAddressDetail As BC  with(nolock)
                                            Inner join CityMaster As D  with(nolock) on BC.CityId = D.CityId
                                            Inner join StateMaster As S  with(nolock) on S.StateId = D.StateId
                                            Inner join CountryMaster As C  with(nolock) on C.CountryId = S.CountryId
                                            Where BC.VendorId = @VendorId", para).ConvertToList<VendorAddressDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<VendorChatMaster> GetChatByVendorId(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VendorId", id);
                return new dalc().GetDataTable_Text(@"Select BD.*,CM.ChatName from VendorChatMaster BD with(nolock)
                                                 Inner join ChatNameMaster CM with(nolock) on CM.ChatId=BD.ChatId
                                                 where BD.VendorId = @VendorId and BD.IsActive = 1", para).ConvertToList<VendorChatMaster>().AsQueryable();

            }
            catch (Exception e)
            {
                throw e;
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
