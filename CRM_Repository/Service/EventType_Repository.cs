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
    public class EventType_Repository : IEventType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public EventType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }


        public void AddEventType(EventTypeMaster EventType)
        {
            try
            {
                context.EventTypeMasters.Add(EventType);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CheckEventType(EventTypeMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@EventTypeName", obj.EventTypeName);
                    para[1] = new SqlParameter().CreateParameter("@EventTypeId", obj.EventTypeId);
                    return new dalc().GetDataTable_Text("SELECT * FROM EventTypeMaster with(nolock) WHERE RTRIM(LTRIM(EventTypeName)) = RTRIM(LTRIM(@EventTypeName)) AND EventTypeId != @EventTypeId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {

                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@EventTypeName", obj.EventTypeName);
                    return new dalc().GetDataTable_Text("SELECT * FROM EventTypeMaster with(nolock) WHERE RTRIM(LTRIM(EventTypeName)) = RTRIM(LTRIM(@EventTypeName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<EventTypeMaster> GetAllEvent()
        {
            try
            {


                return new dalc().selectbyquerydt("SELECT * FROM EventTypeMaster with(nolock) ").ConvertToList<EventTypeMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public EventTypeMaster GetEventById(int id)
        {
            try
            {
               
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@EventTypeId", id);
                    return new dalc().GetDataTable_Text("SELECT * FROM EventTypeMaster with(nolock) WHERE EventTypeId=@EventTypeId", para).ConvertToList<EventTypeMaster>().FirstOrDefault();

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateEventType(EventTypeMaster EventType)
        {
            try
            {
                context.Entry(EventType).State = System.Data.Entity.EntityState.Modified;
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
