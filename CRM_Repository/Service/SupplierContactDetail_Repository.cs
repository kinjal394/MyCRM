using CRM_Repository.Data;
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
    public class SupplierContactDetail_Repository : ISupplierContactDetail_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();

        public SupplierContactDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddSupplierContactDetail(SupplierContactDetail obj)
        {
            try
            {
                context.SupplierContactDetails.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSupplierContactDetail(SupplierContactDetail obj)
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

        public void DeleteSupplierContactDetail(int id)
        {
            try
            {
                SupplierContactDetail SupplierContact = context.SupplierContactDetails.Find(id);
                if (SupplierContact != null)
                {
                    context.SupplierContactDetails.Remove(SupplierContact);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SupplierContactDetail GetById(int ContactId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SupplierContact = context.SupplierContactDetails.Find(id);
                //    scope.Complete();
                //    return SupplierContact;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactId", ContactId);
                return new dalc().GetDataTable_Text("SELECT * FROM SupplierContactDetail with(nolock) WHERE ContactId=@ContactId", para).ConvertToList<SupplierContactDetail>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<SupplierContactDetail> GetAllSupplierContactDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SupplierContactDetails = context.SupplierContactDetails;
                //    scope.Complete();
                //    return SupplierContactDetails;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM SupplierContactDetail with(nolock) ").ConvertToList<SupplierContactDetail>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<SupplierContactDetail> GetBySupplierId(int SupplierId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var SupplierContactBySupplier = context.SupplierContactDetails.Where(z => z.SupplierId == id && z.IsActive == true);
                //    //                    var SupplierContactBySupplier = odal.selectbyquerydt(@"Select SC.*,CM.CountryId[MobileCode],CM.CountryCallCode[CountryCallCode1]
                //    //from SupplierContactDetail As SC  with(nolock)
                //    //Inner join CountryMaster As  CM with(nolock)on CM.CountryId = SC.MobileCode
                //    // where SC.SupplierId = '" + id + "' and SC.IsActive='true'").ConvertToList<SupplierContactDetail>().AsQueryable();
                //    scope.Complete();
                //    return SupplierContactBySupplier;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SupplierId", SupplierId);
                return new dalc().GetDataTable_Text("SELECT * FROM SupplierContactDetail with(nolock) WHERE SupplierId=@SupplierId AND IsActive=1 ", para).ConvertToList<SupplierContactDetail>().AsQueryable();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IQueryable<SupplierContactDetail> GetByContactId(int ContactId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ContactId", ContactId);
                return new dalc().GetDataTable_Text(@"select B.BuyerId As SupplierId,B.ContactId,B.ContactPerson,B.Surname,B.MobileNo,B.Email,A.webaddress
                                                      ,(SELECT STUFF(( 
                                                      SELECT Distinct ',' + l1.LicenseName + ':' + l2.LicenseNo
                                                      from LicenseMaster l1 inner join BuyerLicenseDetail l2 on l1.LicenseId = l2.LicenseId
                                                      Where l2.BuyerId = B.BuyerId FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As TaxDetais
                                                      from BuyerContactDetail As B 
                                                      inner join BuyerAddressDetail as A on A.BuyerId=B.BuyerId
                                                      inner join BuyerLicenseDetail As L on L.BuyerId=B.BuyerId
                                                      inner join LicenseMaster As LM on LM.LicenseId=L.LicenseId
                                                      WHERE ContactId=@ContactId AND B.IsActive=1 
                                                      group by ContactId,B.BuyerId,A.webaddress,B.ContactPerson,B.MobileNo,B.Email,B.Surname
                                                      ", para).ConvertToList<SupplierContactDetail>().AsQueryable();

            }
            catch (Exception e)
            { 
                throw e;
            }
        }

        public IQueryable<SupplierAddressMaster> GetAddressBySupplierId(int SupplierId)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SupplierId", SupplierId);
                return odal.GetDataTable_Text(@"Select BC.*,D.CityId,D.CityName,S.StateId,S.StateName,C.CountryId,C.CountryName from SupplierAddressMaster As BC  with(nolock)
                                            Inner join CityMaster As D  with(nolock) on BC.CityId = D.CityId
                                            Inner join StateMaster As S  with(nolock) on S.StateId = D.StateId
                                            Inner join CountryMaster As C  with(nolock) on C.CountryId = S.CountryId
                                            Where BC.SupplierId =@SupplierId ",para).ConvertToList<SupplierAddressMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<SupplierChatMaster> GetChatBySupplierId(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Id", Id);
                return odal.GetDataTable_Text(@"Select BD.*,CM.ChatName from SupplierChatMaster BD with(nolock)
                                                 Inner join ChatNameMaster CM with(nolock) on CM.ChatId=BD.ChatId
                                                 where BD.SupplierId =@Id and BD.IsActive='true'",para).ConvertToList<SupplierChatMaster>().AsQueryable();
                //return SupplierChatMaster;
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
