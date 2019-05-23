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
    public class HolidayName_Repository : IHolidayName_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public HolidayName_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveHolidayName(HolidayNameMaster objHolidayName)
        {
            try
            {
                context.HolidayNameMasters.Add(objHolidayName);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateHolidayName(HolidayNameMaster objHolidayName)
        {
            try
            {
                context.Entry(objHolidayName).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteHolidayName(int HolidayId)
        {
            try
            {
                HolidayNameMaster objHolidayName = context.HolidayNameMasters.Where(z => z.HolidayId == HolidayId).SingleOrDefault();
                objHolidayName.IsActive = false;
                context.Entry(objHolidayName).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HolidayNameMaster GetByHolidayId(int HolidayId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@HolidayId", HolidayId);
                return new dalc().GetDataTable_Text("SELECT * FROM HolidayNameMaster with(nolock) WHERE HolidayId=@HolidayId ", para).ConvertToList<HolidayNameMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IQueryable<HolidayNameMaster> GetHolidayName()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM HolidayNameMaster with(nolock)").ConvertToList<HolidayNameMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool IsExist(int HolidayId, string HolidayName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@HolidayName", HolidayName);
                para[1] = new SqlParameter().CreateParameter("@HolidayId", HolidayId);
                return new dalc().GetDataTable_Text("SELECT * FROM HolidayNameMaster with(nolock) WHERE RTRIM(LTRIM(HolidayName)) =RTRIM(LTRIM(@HolidayName)) AND HolidayId <> @HolidayId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

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
