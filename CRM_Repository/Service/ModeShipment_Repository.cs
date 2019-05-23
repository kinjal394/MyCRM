using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class ModeShipment_Repository : IModeShipment_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public ModeShipment_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddShip(ShipmentMaster obj)
        {
            try
            {
                context.ShipmentMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void UpdateShip(ShipmentMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ShipmentMaster GetShipById(int ShipmentId)
        {
            try
            {
                //return context.ShipmentMasters.Find(id);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ShipmentId", ShipmentId);
                return new dalc().GetDataTable_Text("SELECT * FROM ShipmentMaster with(nolock) WHERE ShipmentId =@ShipmentId AND IsActive = 1",para).ConvertToList<ShipmentMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<ShipmentMaster> GetAllShipment()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM ShipmentMaster with(nolock) WHERE IsActive = 1").ConvertToList<ShipmentMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ShipmentMaster> DuplicateEditShip(int ShipmentId, string ModeOfShipment)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ShipmentId", ShipmentId);
                para[1] = new SqlParameter().CreateParameter("@ModeOfShipment", ModeOfShipment);
                return new dalc().GetDataTable_Text("SELECT * FROM ShipmentMaster with(nolock) WHERE ShipmentId <> @ShipmentId AND RTRIM(LTRIM( ModeOfShipment)) =RTRIM(LTRIM( @ModeOfShipment)) AND IsActive = 1", para).ConvertToList<ShipmentMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<ShipmentMaster> DuplicateShip(string ModeOfShipment)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ModeOfShipment", ModeOfShipment);
                return new dalc().GetDataTable_Text("SELECT * FROM ShipmentMaster with(nolock) WHERE  RTRIM(LTRIM( ModeOfShipment)) =RTRIM(LTRIM( @ModeOfShipment)) AND IsActive = 1", para).ConvertToList<ShipmentMaster>().AsQueryable();
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
