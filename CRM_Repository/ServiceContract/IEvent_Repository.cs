using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
namespace CRM_Repository.ServiceContract
{
    public interface IEvent_Repository : IDisposable
    {
        IQueryable<EventTypeMaster> getEventTypes();
        IQueryable<EventMaster> getAllEvent();
        void AddEvent(EventMaster eventobj);
        void UpdateEvent(EventMaster eventobj);
        void DeleteEvent(int id);
        EventMaster getEventbyid(int id);
        bool CheckEvent(EventMaster obj, bool isUpdate);
    }
}
