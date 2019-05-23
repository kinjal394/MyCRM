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
    public class Event_Repository : IEvent_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Event_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddEvent(EventMaster eventobj)
        {
            try
            {
                context.EventMasters.Add(eventobj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool CheckEvent(EventMaster obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    //var data = context.EventMasters.Where(e => e.EventName.Trim() == obj.EventName.Trim() && e.EventTypeId == obj.EventTypeId && e.EventId != obj.EventId && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;
                    SqlParameter[] para = new SqlParameter[3];
                    para[0] = new SqlParameter().CreateParameter("@EventName", obj.EventName);
                    para[1] = new SqlParameter().CreateParameter("@EventId", obj.EventId);
                    para[2] = new SqlParameter().CreateParameter("@EventTypeId", obj.EventTypeId.ToString());
                    return new dalc().GetDataTable_Text("SELECT * FROM EventMaster with(nolock) WHERE RTRIM(LTRIM(EventName)) = RTRIM(LTRIM(@EventName)) AND EventId <> @EventId AND EventTypeId=@EventTypeId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    //var data = context.EventMasters.Where(e => e.EventName.Trim() == obj.EventName.Trim() && e.EventTypeId == obj.EventTypeId && e.IsActive == true).ToList();
                    //return data.Count > 0 ? true : false;

                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@EventName", obj.EventName);
                    para[1] = new SqlParameter().CreateParameter("@EventTypeId", obj.EventTypeId.ToString());
                    return new dalc().GetDataTable_Text("SELECT * FROM EventMaster with(nolock) WHERE RTRIM(LTRIM(EventName)) = RTRIM(LTRIM(@EventName)) AND  EventTypeId=@EventTypeId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteEvent(int id)
        {
            try
            {
                EventMaster obj = context.EventMasters.Find(id);
                if (obj != null)
                {
                    context.EventMasters.Remove(obj);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<EventMaster> getAllEvent()
        {
            try
            {
               
                return new dalc().selectbyquerydt("SELECT * FROM EventMaster with(nolock) WHERE  IsActive = 1").ConvertToList<EventMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public EventMaster getEventbyid(int Eventid)
        {
            try
            {
              

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Eventid", Eventid);
                return new dalc().GetDataTable_Text("SELECT * FROM EventMaster with(nolock) WHERE Eventid=@Eventid ", para).ConvertToList<EventMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<EventTypeMaster> getEventTypes()
        {
            try
            {
               
                return new dalc().selectbyquerydt("SELECT * FROM EventTypeMaster with(nolock) WHERE  IsActive = 1").ConvertToList<EventTypeMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateEvent(EventMaster eventobjobj)
        {
            try
            {
                context.Entry(eventobjobj).State = System.Data.Entity.EntityState.Modified;
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
