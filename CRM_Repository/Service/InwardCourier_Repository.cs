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
    public class InwardCourier_Repository : IInwardCourier_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        dalc odal = new dalc();
        public InwardCourier_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveInwardCourier(InwardCourierMaster objInwardCourier) 
        {
            try
            {
                context.InwardCourierMasters.Add(objInwardCourier);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateInwardCourier(InwardCourierMaster objInwardCourier)
        {
            try
            {
                context.Entry(objInwardCourier).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteInwardCourier(int CourierId, int UserId)
        {
            try
            {
                InwardCourierMaster objInwardCourier = context.InwardCourierMasters.Where(z => z.CourierId == CourierId).SingleOrDefault();
                objInwardCourier.IsActive = false;
                objInwardCourier.DeletedBy = UserId;
                objInwardCourier.DeletedDate = DateTime.Now;
                context.Entry(objInwardCourier).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public InwardCourierMaster GetByCourierId(int CourierId)
        {
            try
            {
              
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CourierId", CourierId);
                return new dalc().GetDataTable_Text("SELECT * FROM InwardCourierMaster with(nolock) WHERE CourierId=@CourierId ", para).ConvertToList<InwardCourierMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public InwardCourierMaster FetchAllInfoById(int CourierId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CourierId", CourierId);
                //return odal.GetDataTable_Text(@"select OCM.CourierId,OCM.CourierDate,OCM.CourierTime,OCM.VendorId,VM.CompanyName as Vendor,OCM.SenderId,
                //                               OCM.ReceivedBy,U.UserName As Receiver,OCM.CourierReffNo,OCM.CourierTypeId,CSM.CourierType,
                //                               Case OCM.SenderType WHEN 'v' THEN VMM.CompanyName WHEN 'S' THEN  SM.CompanyName  WHEN 'B' THEN  BM.CompanyName  ELSE null  END as Sender,OCM.ShipmentRemark,OCM.SenderType,OCM.ShipmentRefNo,OCM.POD,OCM.ShipmentPhoto
                //                               from InwardCourierMaster as OCM 
                //                               Inner join UserMaster As U  with(nolock) on OCM.ReceivedBy = U.UserId
                //                               left join VendorMaster as VM with(nolock) on VM.VendorId = OCM.VendorId 
                //                               left join SupplierMaster SM with(nolock) on SM.SupplierId = OCM.SenderId AND OCM.SenderType = 'S' 
                //                               left join VendorMaster VMM with(nolock) on VMM.VendorId = OCM.SenderId AND OCM.SenderType = 'V' 
                //                               left join BuyerMaster BM with(nolock) on BM.BuyerId = OCM.SenderId AND OCM.SenderType = 'B'
                //                               left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = OCM.CourierTypeId
                //                               Where OCM.CourierId = @CourierId AND ISNULL(OCM.IsActive,0)=1", para).ConvertToList<InwardCourierMaster>().AsQueryable().FirstOrDefault();

                return odal.GetDataTable_Text(@"select OCM.CourierId,OCM.CourierDate,OCM.CourierTime,OCM.VendorId,VM.CompanyName as Vendor,OCM.SenderId,
                                                OCM.ReceivedBy,U.UserName As Receiver,OCM.CourierReffNo,OCM.CourierTypeId,CSM.CourierType,BM.CompanyName as Sender,
                                                OCM.ShipmentRemark,OCM.SenderType,OCM.ShipmentRefNo,OCM.POD,OCM.ShipmentPhoto
                                                from InwardCourierMaster as OCM 
                                                Inner join UserMaster As U  with(nolock) on OCM.ReceivedBy = U.UserId
                                                Inner join VendorMaster as VM with(nolock) on VM.VendorId = OCM.VendorId 
                                                Inner join BuyerMaster BM with(nolock) on BM.BuyerId = OCM.SenderId 
                                                left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = OCM.CourierTypeId
                                                Where OCM.CourierId = @CourierId AND ISNULL(OCM.IsActive,0)=1", para).ConvertToList<InwardCourierMaster>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<InwardCourierMaster> GetInwardCourier()
        {
            try
            {
              
                return new dalc().selectbyquerydt("SELECT * FROM InwardCourierMaster with(nolock) WHERE IsActive = 1").ConvertToList<InwardCourierMaster>().AsQueryable();
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
