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
    public class BillofLoading_Repository: IBillofLoading_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public BillofLoading_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
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

        public void AddBillofLoading(BillofLoadingMaster obj)
        {
            try
            {
                context.BillofLoadingMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateBillofLoading(BillofLoadingMaster obj)
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

        public void DeleteBillofLoading(int id)
        {
            try
            {
                //AgencyTypeMaster AgencyType = context.AgencyTypeMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BLId", id);
                BillofLoadingMaster obj = new dalc().GetDataTable_Text("SELECT * FROM BillofLoadingMaster with(nolock) WHERE BLId=@BLId", para).ConvertToList<BillofLoadingMaster>().FirstOrDefault();
                if (obj != null)
                {
                    //context.BillofLoadingMasters.Remove(obj);
                    //context.SaveChanges();
                    obj.Date = DateTime.Now;
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

        public BillofLoadingMaster GetBillofLoadingID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BLId", id);
                var obj = new dalc().GetDataTable_Text("SELECT * FROM BillofLoadingMaster with(nolock) WHERE BLId=@BLId", para).ConvertToList<BillofLoadingMaster>().FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<BillofLoadingMaster> GetAllBillofLoading()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var obj = new dalc().GetDataTable_Text("SELECT * FROM BillofLoadingMaster with(nolock)", para).ConvertToList<BillofLoadingMaster>().AsQueryable();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<BillofLoadingMaster> DuplicateBillofLoading(string ShipperName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ShipperName", ShipperName);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var obj = new dalc().GetDataTable_Text("SELECT * FROM BillofLoadingMaster with(nolock) WHERE ShipperName=@ShipperName", para).ConvertToList<BillofLoadingMaster>().AsQueryable();
                return obj.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<BillofLoadingMaster> DuplicateEditBillofLoading(int BLId, string ShipperName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@BLId", BLId);
                para[1] = new SqlParameter().CreateParameter("@ShipperName", ShipperName);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var obj = new dalc().GetDataTable_Text("SELECT * FROM BillofLoadingMaster with(nolock) WHERE BLId!=@BLId and ShipperName=@ShipperName", para).ConvertToList<BillofLoadingMaster>().AsQueryable();
                return obj.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
    }
}
