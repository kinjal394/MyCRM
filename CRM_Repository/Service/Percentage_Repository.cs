using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class Percentage_Repository : IPercentage_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Percentage_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddPercentage(PercentageMaster obj)
        {
            try
            {
                context.PercentageMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public PercentageMaster GetPercentageById(int PercentageId)
        {
            try
            {
                //return context.PercentageMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PercentageId", PercentageId);
                return new dalc().GetDataTable_Text("SELECT * FROM PercentageMaster with(nolock) WHERE PercentageId=@PercentageId", para).ConvertToList<PercentageMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<PercentageMaster> DuplicateEditPercentage(int PercentageId, decimal Percentage)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@Percentage", Percentage);
                para[1] = new SqlParameter().CreateParameter("@PercentageId", PercentageId);
                return new dalc().GetDataTable_Text("SELECT * FROM PercentageMaster with(nolock) WHERE Percentage = @Percentage AND PercentageId <> @PercentageId AND IsActive = 1", para).ConvertToList<PercentageMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<PercentageMaster> DuplicatePercentage(decimal Percentage)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Percentage", Percentage);
                return new dalc().GetDataTable_Text("SELECT * FROM PercentageMaster with(nolock) WHERE Percentage = @Percentage AND IsActive = 1", para).ConvertToList<PercentageMaster>().AsQueryable();


            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdatePercentage(PercentageMaster obj)
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

        public IQueryable<PercentageMaster> GetAllPercentage()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM PercentageMaster with(nolock) WHERE  IsActive = 1").ConvertToList<PercentageMaster>().AsQueryable();

            }
            catch
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
