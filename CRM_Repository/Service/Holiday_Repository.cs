using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;

using System.Data.SqlClient;
using CRM_Repository.DataServices;
namespace CRM_Repository.Service
{
    public class Holiday_Repository : IHoliday_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Holiday_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddHoliday(HolidayMaster holiday)
        {
            try
            {
                context.HolidayMasters.Add(holiday);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool CheckHoliday(HolidayMaster obj)
        {
            try
            {
                if (obj.HolidayId != default(int))
                {
                   SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@HolidayNameId", obj.HolidayNameId);
                    para[1] = new SqlParameter().CreateParameter("@HolidayId", obj.HolidayId);
                    return new dalc().GetDataTable_Text("SELECT * FROM HolidayMaster with(nolock) WHERE HolidayNameId =@HolidayNameId AND HolidayId <> @HolidayId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                     SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@HolidayNameId", obj.HolidayNameId);
                    return new dalc().GetDataTable_Text("SELECT * FROM HolidayMaster with(nolock) WHERE HolidayNameId != @HolidayNameId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public IQueryable<HolidayMaster> GetAllHoliday()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM HolidayNameMaster with(nolock) ").ConvertToList<HolidayMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<HolidayNameMaster> GetAllHolidayName()
        {
            try
            {
                
                return new dalc().selectbyquerydt("SELECT * FROM HolidayNameMaster with(nolock) ").ConvertToList<HolidayNameMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public HolidayMaster GetHolidayById(int id)
        {
            try
            {
               
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@HolidayId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM HolidayMaster with(nolock) WHERE HolidayId=@HolidayId ", para).ConvertToList<HolidayMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateHoliday(HolidayMaster data)
        {
            try
            {
                context.Entry(data).State = System.Data.Entity.EntityState.Modified;
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
