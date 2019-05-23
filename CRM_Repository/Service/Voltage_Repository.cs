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
    public class Voltage_Repository:IVoltage_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Voltage_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddVoltage(VoltageMaster obj)
        {
            try
            {
                context.VoltageMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateVoltage(VoltageMaster obj)
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

        public void DeleteVoltage(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VoltageId", id);
                VoltageMaster Voltage = new dalc().GetDataTable_Text("SELECT * FROM VoltageMaster with(nolock) WHERE VoltageId=@VoltageId", para).ConvertToList<VoltageMaster>().FirstOrDefault();
                if (Voltage != null)
                {
                    context.VoltageMasters.Remove(Voltage);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public VoltageMaster GetVoltageByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VoltageId", id);
                var Voltage = new dalc().GetDataTable_Text("SELECT * FROM VoltageMaster with(nolock) WHERE VoltageId=@VoltageId", para).ConvertToList<VoltageMaster>().FirstOrDefault();
                return Voltage;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VoltageMaster> GetAllVoltage()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Voltage = new dalc().GetDataTable_Text("SELECT * FROM VoltageMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<VoltageMaster>().AsQueryable();
                return Voltage;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VoltageMaster> DuplicateVoltage(string Voltage)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@Voltage", Voltage);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Voltagedata = new dalc().GetDataTable_Text("SELECT * FROM VoltageMaster with(nolock) WHERE Voltage=@Voltage and IsActive=@IsActive", para).ConvertToList<VoltageMaster>().AsQueryable();
                return Voltagedata.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VoltageMaster> DuplicateEditVoltage(int VoltageId, string Voltage)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@VoltageId", VoltageId);
                para[1] = new SqlParameter().CreateParameter("@Voltage", Voltage);
                para[2] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Voltagedata = new dalc().GetDataTable_Text("SELECT * FROM VoltageMaster with(nolock) WHERE VoltageId!=@VoltageId and Voltage=@Voltage and IsActive=@IsActive", para).ConvertToList<VoltageMaster>().AsQueryable();
                return Voltagedata.AsQueryable();
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
