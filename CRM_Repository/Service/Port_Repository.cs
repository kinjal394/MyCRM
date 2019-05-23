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
    public class Port_Repository : IPort_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Port_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddPort(PortMaster obj)
        {
            try
            {
                context.PortMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<PortMaster> GetAllPort()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM PortMaster with(nolock) ").ConvertToList<PortMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<PortMaster> DuplicateEditPort(int PortId, string PortName)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@PortId", PortId);
                para[1] = new SqlParameter().CreateParameter("@PortName", PortName);
                return new dalc().GetDataTable_Text("SELECT * FROM PortMaster with(nolock) WHERE PortId <> @PortId AND RTRIM(LTRIM(PortName)) = RTRIM(LTRIM(@PortName)) AND IsActive = 1", para).ConvertToList<PortMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<PortMaster> DuplicatePort(string Portname)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Portname", Portname);
                return new dalc().GetDataTable_Text("SELECT * FROM PortMaster with(nolock) WHERE RTRIM(LTRIM(Portname)) = RTRIM(LTRIM(@Portname)) AND IsActive = 1", para).ConvertToList<PortMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public IQueryable<PortMaster> getAllPort()
        //{
        //    try
        //    {
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
        //        {
        //            PortMaster pmaster = new PortMaster();
        //            pmaster = context.PortMasters.ToList;
        //            scope.Complete();
        //            return pmaster; 
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        public PortMaster GetPortById(int PortId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PortId", PortId);
                return new dalc().GetDataTable_Text("SELECT * FROM PortMaster with(nolock) WHERE PortId = @PortId", para).ConvertToList<PortMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }



        public void UpdatePort(PortMaster obj)
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
