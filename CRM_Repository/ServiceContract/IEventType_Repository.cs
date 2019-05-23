using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
namespace CRM_Repository.ServiceContract
{
    public interface IEventType_Repository : IDisposable
    {
        void AddEventType(EventTypeMaster EventType);
        void UpdateEventType(EventTypeMaster EventType);
        EventTypeMaster GetEventById(int id);
        IQueryable<EventTypeMaster> GetAllEvent();
        bool CheckEventType(EventTypeMaster obj ,bool isUpdate);
    }
}
