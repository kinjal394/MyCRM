using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class BuyerContactDetail_Repository : IBuyerContactDetail_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public BuyerContactDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddBuyerContactDetail(BuyerContactDetail obj)
        {
            try
            {
                context.BuyerContactDetails.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBuyerContactDetail(BuyerContactDetail obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteBuyerContactDetail(int id)
        {
            try
            {
                BuyerContactDetail BuyerContact = context.BuyerContactDetails.Find(id);
                if (BuyerContact != null)
                {
                    context.BuyerContactDetails.Remove(BuyerContact);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<vmBuyerContactDetail> GetById(int BuyerId)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", BuyerId);
                return odal.GetDataTable_Text(@"Select BC.*,D.DesignationId,D.DesignationName from BuyerContactDetail As BC  with(nolock)
                                            Left join DesignationMaster As D  with(nolock) on BC.DesignationId = D.DesignationId
                                            Where BC.BuyerId =@BuyerId And ISNULL(BC.IsActive,0)=1", para).ConvertToList<vmBuyerContactDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerAddressDetail> GetAddressByBuyerId(int BuyerId)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", BuyerId);
                return odal.GetDataTable_Text(@"Select BC.*,ad.AddressTypeName[AddressType],D.CityId,D.CityName,S.StateId,S.StateName,C.CountryId,C.CountryName from BuyerAddressDetail As BC  with(nolock)
                                            Inner join CityMaster As D  with(nolock) on BC.CityId = D.CityId
                                            Inner join StateMaster As S  with(nolock) on S.StateId = D.StateId
                                            Inner join CountryMaster As C  with(nolock) on C.CountryId = S.CountryId
                                            left join AddressTypeMaster As ad  with(nolock) on ad.AddressTypeId = BC.AddressTypeId
                                            Where BC.BuyerId =@BuyerId ", para).ConvertToList<BuyerAddressDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerContactDetail> GetContactDetailbybuyerId(int id)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return odal.GetDataTable_Text(@" select * from BuyerContactDetail where buyerId=@BuyerId and ISNULL(IsActive,0)=1", para).ConvertToList<BuyerContactDetail>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<BuyerContactDetail> GetContactDetailbyContactId(int id)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactId", id);
                return odal.GetDataTable_Text(@" select * from BuyerContactDetail where ContactId=@ContactId and ISNULL(IsActive,0)=1", para).ConvertToList<BuyerContactDetail>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<BuyerContactDetail> GetAllBuyerContactDetail()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM BuyerContactDetail with(nolock) ").ConvertToList<BuyerContactDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerContactDetail> GetByBuyerId(int id)
        {
            dalc odal = new dalc();
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerContactDetail with(nolock) WHERE BuyerId=@BuyerId AND IsActive = 1", para).ConvertToList<BuyerContactDetail>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<BuyerLicenseDetail> GetLicenseByBuyerId(int id)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return odal.GetDataTable_Text(@"Select BL.*,LM.LicenseName from BuyerLicenseDetail As BL with(nolock)
                                            Inner join LicenseMaster As LM with(nolock) On BL.LicenseId = LM.LicenseId
                                            Where BL.BuyerId =@BuyerId ", para).ConvertToList<BuyerLicenseDetail>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BuyerContactDetail GetBCbyid(int BuyerId)
        {
            dalc odal = new dalc();
            try
            {
              
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", BuyerId);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerContactDetail with(nolock) WHERE BuyerId=@BuyerId ", para).ConvertToList<BuyerContactDetail>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public BuyerAddressDetail GetBAddbyid(int BAddId)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BAddId", BAddId);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerAddressDetail with(nolock) WHERE AddressId=@BAddId ", para).ConvertToList<BuyerAddressDetail>().FirstOrDefault();
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
