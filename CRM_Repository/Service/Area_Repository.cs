using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM_Repository.Service
{
    public class Area_Repository : IArea_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Area_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void SaveArea(AreaMaster Area)
        {
            try
            {
                context.AreaMasters.Add(Area);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateArea(AreaMaster Area)
        {
            try
            {
                context.Entry(Area).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void Delete(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AreaId", id);
                AreaMaster Area = new dalc().GetDataTable_Text("SELECT * FROM AreaMaster with(nolock) WHERE AreaId=@AreaId", para).ConvertToList<AreaMaster>().FirstOrDefault();
                Area.IsActive = false;
                context.Entry(Area).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public AreaMaster GetAreaById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@AreaId", id);
                var Area = new dalc().GetDataTable_Text("SELECT * FROM AreaMaster with(nolock) WHERE AreaId=@AreaId", para).ConvertToList<AreaMaster>().FirstOrDefault();
                //var Area = context.AreaMasters.Find(id);
                return Area;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<AreaMaster> GetArea()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM AreaMaster with(nolock)").ConvertToList<AreaMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public IQueryable<AreaMaster> GetAreaByCityId(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CityId", id);
                para[1] = new SqlParameter().CreateParameter("@IsActive", "true");
                var Area = new dalc().GetDataTable_Text("SELECT * FROM AreaMaster with(nolock) WHERE CityId=@CityId and IsActive=@IsActive", para).ConvertToList<AreaMaster>().AsQueryable();
                return Area;
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
