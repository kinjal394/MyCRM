using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System.Data.SqlClient;
using CRM_Repository.DataServices;

namespace CRM_Repository.Service
{
   public class WorkRemind_Repository:IWorkReminder_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public WorkRemind_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddWorkReminder(WorkReminderMaster obj)
        {
            try
            {
                context.WorkReminderMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdateWorkReminder(WorkReminderMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void DeleteWorkReminder(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@WorkRemindId", id);
                WorkReminderMaster WorkReminder = new dalc().GetDataTable_Text("SELECT * FROM WorkReminderMaster with(nolock) WHERE WorkRemindId=@WorkRemindId", para).ConvertToList<WorkReminderMaster>().FirstOrDefault();
                if (WorkReminder != null)
                {
                    context.WorkReminderMasters.Remove(WorkReminder);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public WorkReminderMaster GetWorkReminderByID(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@WorkRemindId", id);
                var WorkReminder = new dalc().GetDataTable_Text("SELECT * FROM WorkReminderMaster with(nolock) WHERE WorkRemindId=@WorkRemindId", para).ConvertToList<WorkReminderMaster>().FirstOrDefault();
                return WorkReminder;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<WorkReminderMaster> GetAllWorkReminder()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@IsActive", "true");
                var WorkReminder = new dalc().GetDataTable_Text("SELECT * FROM WorkReminderMaster with(nolock) WHERE IsActive=@IsActive", para).ConvertToList<WorkReminderMaster>().AsQueryable();
                return WorkReminder;
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
