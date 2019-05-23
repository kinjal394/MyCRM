using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
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
    public class OutwardCourier_Reposiroty : IOutwardCourier_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;

        public OutwardCourier_Reposiroty(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveOutwardCourier(OutwardCourierMaster objOutwardCourier)
        {
            try
            {
                context.OutwardCourierMasters.Add(objOutwardCourier);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOutwardCourier(OutwardCourierMaster objOutwardCourier)
        {
            try
            {
                context.Entry(objOutwardCourier).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOutwardCourier(int CourierId, int UserId)
        {
            try
            {
                OutwardCourierMaster objOutwardCourier = context.OutwardCourierMasters.Where(z => z.CourierId == CourierId).SingleOrDefault();
                objOutwardCourier.IsActive = false;
                objOutwardCourier.DeletedBy = UserId;
                objOutwardCourier.DeletedDate = DateTime.Now;
                context.Entry(objOutwardCourier).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OutwardCourierMaster GetByCourierId(int CourierId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CourierId", CourierId);
                return new dalc().GetDataTable_Text("SELECT * FROM OutwardCourierMaster with(nolock) WHERE CourierId=@CourierId ", para).ConvertToList<OutwardCourierMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IQueryable<OutwardCourierMaster> GetOutwardCourier()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM OutwardCourierMaster with(nolock) WHERE IsActive = 1").ConvertToList<OutwardCourierMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public OutwardCourierModel FetchAllInfoById(int CourierId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CourierId", CourierId);//
                                                                                      //return odal.GetDataTable_Text(@"select OCM.CourierId,OCM.CourierDate,OCM.CourierTime,OCM.VendorId,VM.CompanyName as Vendor,OCM.ReceiverId,OCM.ReceiverAddress,OCM.ReceiverAddressId,
                                                                                      //                               OCM.SenderBy,U.UserName As Sender,CT.CityId,CT.CityName,S.StateId,S.StateName,CO.CountryId,CO.CountryName,OCM.CourierTypeId,CSM.CourierType,OCM.CourierReffNo,
                                                                                      //                               Case  OCM.ReceiverType WHEN 'v'  THEN VMM.CompanyName WHEN 'S' THEN  SM.CompanyName  WHEN 'B' THEN  BM.CompanyName  ELSE null  END as Receiver, OCM.ReceiverType,
                                                                                      //                               OCM.ShipmentRefNo,OCM.Amount,OCM.PaymentBy,OCM.Remark,OCM.POD,OCM.ShipmentPhoto,OCM.CurrencyId,CCM.CurrencyName
                                                                                      //                               from OutwardCourierMaster as OCM 
                                                                                      //                               left join UserMaster As U  with(nolock) on OCM.SenderBy = U.UserId
                                                                                      //                               Inner join VendorMaster as VM with(nolock) on VM.VendorId = OCM.VendorId 
                                                                                      //                               Inner join BuyerAddressDetail BAM with(nolock) on BAM.AddressId = OCM.ReceiverAddressId
                                                                                      //                               Inner join CityMaster As CT with(nolock)on CT.CityId = BAM.CityId
                                                                                      //                               Inner join StateMaster As S  with(nolock) on S.StateId = CT.StateId
                                                                                      //                               Inner join CountryMaster As  CO with(nolock)on Co.CountryId = S.CountryId
                                                                                      //                               left join SupplierMaster SM with(nolock) on SM.SupplierId = OCM.ReceiverId AND OCM.ReceiverType = 'S' 
                                                                                      //                               left join VendorMaster VMM with(nolock) on VMM.VendorId = OCM.ReceiverId AND OCM.ReceiverType = 'V' 
                                                                                      //                               left join BuyerMaster BM with(nolock) on BM.BuyerId = OCM.ReceiverId AND OCM.ReceiverType = 'B'
                                                                                      //                               left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = OCM.CourierTypeId
                                                                                      //                               left join Currencymaster CCM with(nolock) on CCM.CurrencyId = OCM.CurrencyId
                                                                                      //                               Where OCM.CourierId =@CourierId AND ISNULL(OCM.IsActive,0)=1", para).ConvertToList<OutwardCourierModel>().AsQueryable().FirstOrDefault();
                return odal.GetDataTable_Text(@"select OCM.CourierId,OCM.CourierDate,OCM.CourierTime,OCM.VendorId,VM.CompanyName as Vendor,OCM.ReceiverId,OCM.ReceiverAddress,OCM.ReceiverAddressId,
                                                OCM.SenderBy,U.UserName As Sender,CT.CityId,CT.CityName,S.StateId,S.StateName,CO.CountryId,CO.CountryName,OCM.CourierTypeId,CSM.CourierType,OCM.CourierReffNo,
                                                BM.CompanyName as Receiver,
                                                 OCM.ReceiverType,
                                                OCM.ShipmentRefNo,OCM.Amount,OCM.PaymentBy,OCM.Remark,OCM.POD,OCM.ShipmentPhoto,OCM.CurrencyId,CCM.CurrencyName
                                                from OutwardCourierMaster as OCM 
                                                left join UserMaster As U  with(nolock) on OCM.SenderBy = U.UserId
                                                Inner join VendorMaster as VM with(nolock) on VM.VendorId = OCM.VendorId 
                                                Inner join BuyerAddressDetail BAM with(nolock) on BAM.AddressId = OCM.ReceiverAddressId
                                                Inner join CityMaster As CT with(nolock)on CT.CityId = BAM.CityId
                                                Inner join StateMaster As S  with(nolock) on S.StateId = CT.StateId
                                                Inner join CountryMaster As  CO with(nolock)on Co.CountryId = S.CountryId
                                                left join BuyerMaster BM with(nolock) on BM.BuyerId = OCM.ReceiverId
                                                left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = OCM.CourierTypeId
                                                left join Currencymaster CCM with(nolock) on CCM.CurrencyId = OCM.CurrencyId
                                                Where OCM.CourierId =@CourierId AND ISNULL(OCM.IsActive,0)=1", para).ConvertToList<OutwardCourierModel>().AsQueryable().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateUpdate(OutwardCourierModel objOutwardCourierModel)
        {
            int resVal;
            try
            {
                OutwardCourierMaster objOutwardCourierMaster = new OutwardCourierMaster();
                //if (objOutwardCourierModel.AreaId == 0)
                //{
                //    objOutwardCourierModel.AreaId = 2; // HardCode Set AreaID : 2 
                //}
                if (objOutwardCourierModel.CourierId <= 0)
                {
                    //Add
                    objOutwardCourierMaster.CourierDate = objOutwardCourierModel.CourierDate;
                    objOutwardCourierMaster.CourierTime = objOutwardCourierModel.CourierTime;
                    objOutwardCourierMaster.ReceiverAddressId = objOutwardCourierModel.ReceiverAddressId;
                    objOutwardCourierMaster.ReceiverCompanyName = objOutwardCourierModel.ReceiverCompanyName;
                    objOutwardCourierMaster.ReceiverAddress = objOutwardCourierModel.ReceiverAddress;
                    objOutwardCourierMaster.ReceiverCity = objOutwardCourierModel.ReceiverCity;
                    objOutwardCourierMaster.ReceiverCountry = objOutwardCourierModel.ReceiverCountry;
                    objOutwardCourierMaster.ReceiverState = objOutwardCourierModel.ReceiverState;
                    objOutwardCourierMaster.VendorId = objOutwardCourierModel.VendorId;
                    objOutwardCourierMaster.ReceiverId = objOutwardCourierModel.ReceiverId;
                    objOutwardCourierMaster.SenderBy = objOutwardCourierModel.SenderBy;
                    objOutwardCourierMaster.ReceiverType = objOutwardCourierModel.ReceiverType;
                    objOutwardCourierMaster.ShipmentRefNo = objOutwardCourierModel.ShipmentRefNo;
                    objOutwardCourierMaster.Amount = objOutwardCourierModel.Amount;
                    objOutwardCourierMaster.PaymentBy = objOutwardCourierModel.PaymentBy;
                    objOutwardCourierMaster.Remark = objOutwardCourierModel.Remark;
                    objOutwardCourierMaster.POD = objOutwardCourierModel.POD;
                    objOutwardCourierMaster.ShipmentPhoto = objOutwardCourierModel.ShipmentPhoto;
                    objOutwardCourierMaster.CourierReffNo = objOutwardCourierModel.CourierReffNo;
                    objOutwardCourierMaster.CourierTypeId = objOutwardCourierModel.CourierTypeId;
                    objOutwardCourierMaster.CurrencyId = objOutwardCourierModel.CurrencyId;
                    objOutwardCourierMaster.CreatedBy = objOutwardCourierModel.CreatedBy;
                    objOutwardCourierMaster.CreatedDate = DateTime.Now;
                    objOutwardCourierMaster.IsActive = true;
                    SaveOutwardCourier(objOutwardCourierMaster);
                    resVal = 1;
                }
                else
                {
                    //Edit
                    objOutwardCourierMaster = GetByCourierId(objOutwardCourierModel.CourierId);
                    objOutwardCourierMaster.CourierDate = objOutwardCourierModel.CourierDate;
                    objOutwardCourierMaster.CourierTime = objOutwardCourierModel.CourierTime;
                    objOutwardCourierMaster.ReceiverAddressId = objOutwardCourierModel.ReceiverAddressId;
                    objOutwardCourierMaster.ReceiverCompanyName = objOutwardCourierModel.ReceiverCompanyName;
                    objOutwardCourierMaster.ReceiverAddress = objOutwardCourierModel.ReceiverAddress;
                    objOutwardCourierMaster.ReceiverCity = objOutwardCourierModel.ReceiverCity;
                    objOutwardCourierMaster.ReceiverCountry = objOutwardCourierModel.ReceiverCountry;
                    objOutwardCourierMaster.ReceiverState = objOutwardCourierModel.ReceiverState;
                    objOutwardCourierMaster.VendorId = objOutwardCourierModel.VendorId;
                    objOutwardCourierMaster.ReceiverId = objOutwardCourierModel.ReceiverId;
                    objOutwardCourierMaster.SenderBy = objOutwardCourierModel.SenderBy;
                    objOutwardCourierMaster.ReceiverType = objOutwardCourierModel.ReceiverType;
                    objOutwardCourierMaster.ShipmentRefNo = objOutwardCourierModel.ShipmentRefNo;
                    objOutwardCourierMaster.Amount = objOutwardCourierModel.Amount;
                    objOutwardCourierMaster.PaymentBy = objOutwardCourierModel.PaymentBy;
                    objOutwardCourierMaster.Remark = objOutwardCourierModel.Remark;
                    objOutwardCourierMaster.POD = objOutwardCourierModel.POD;
                    objOutwardCourierMaster.ShipmentPhoto = objOutwardCourierModel.ShipmentPhoto;
                    objOutwardCourierMaster.CourierReffNo = objOutwardCourierModel.CourierReffNo;
                    objOutwardCourierMaster.CourierTypeId = objOutwardCourierModel.CourierTypeId;
                    objOutwardCourierMaster.CurrencyId = objOutwardCourierModel.CurrencyId;
                    objOutwardCourierMaster.ModifyBy = objOutwardCourierModel.ModifyBy;
                    objOutwardCourierMaster.ModifyDate = DateTime.Now;
                    UpdateOutwardCourier(objOutwardCourierMaster);
                    resVal = 2;
                }
            }
            catch (Exception)
            {
                resVal = 0;
            }
            return resVal;
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
