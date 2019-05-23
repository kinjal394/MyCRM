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
    public class ShippingOrder_Repository : IShippingOrder_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ShippingOrder_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddShippingOrder(ShippingOrderMaster obj)
        {
            try
            {
                context.ShippingOrderMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateShippingOrder(ShippingOrderMaster obj)
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
        public void DeleteShippingOrder(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShippingOrdId", id);
                ShippingOrderMaster obj = new dalc().GetDataTable_Text("SELECT * FROM ShippingOrderMaster with(nolock) WHERE ShippingOrdId=@ShippingOrdId", para).ConvertToList<ShippingOrderMaster>().FirstOrDefault();
                if (obj != null)
                {
                    obj.Date= DateTime.Now;
                    obj.IsActive = false;
                    context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public ShippingOrderMaster GetShippingOrderID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShippingOrdId", id);
                var obj = new dalc().GetDataTable_Text("SELECT * FROM ShippingOrderMaster with(nolock) WHERE ShippingOrdId=@ShippingOrdId", para).ConvertToList<ShippingOrderMaster>().FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ShippingOrderMaster> GetAllShippingOrder()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var obj = new dalc().GetDataTable_Text("SELECT * FROM ShippingOrderMaster with(nolock)", para).ConvertToList<ShippingOrderMaster>().AsQueryable();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ShippingOrderMaster> DuplicateShippingOrder(string TypeofShipment)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TypeofShipment", TypeofShipment);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var obj = new dalc().GetDataTable_Text("SELECT * FROM ShippingOrderMaster with(nolock) WHERE TypeofShipment=@TypeofShipment", para).ConvertToList<ShippingOrderMaster>().AsQueryable();
                return obj.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<ShippingOrderMaster> DuplicateEditShippingOrder(int ShippingOrdId, string TypeofShipment)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@ShippingOrdId", ShippingOrdId);
                para[1] = new SqlParameter().CreateParameter("@TypeofShipment", TypeofShipment);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var obj = new dalc().GetDataTable_Text("SELECT * FROM ShippingOrderMaster with(nolock) WHERE ShippingOrdId!=@ShippingOrdId and TypeofShipment=@TypeofShipment", para).ConvertToList<ShippingOrderMaster>().AsQueryable();
                return obj.AsQueryable();
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
