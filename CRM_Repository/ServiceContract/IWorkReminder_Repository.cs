using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IWorkReminder_Repository: IDisposable
    {
        void AddWorkReminder(WorkReminderMaster obj);
        void UpdateWorkReminder(WorkReminderMaster obj);
        void DeleteWorkReminder(int id);
        WorkReminderMaster GetWorkReminderByID(int id);
        IQueryable<WorkReminderMaster> GetAllWorkReminder();
        //IQueryable<WorkReminderMaster> DuplicateWorkReminder(string AgencyType);
        //IQueryable<WorkReminderMaster> DuplicateEditWorkReminder(int WorkRemindId, string AgencyType);
    }
}
