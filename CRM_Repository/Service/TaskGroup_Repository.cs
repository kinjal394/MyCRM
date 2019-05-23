using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using System.Transactions;
namespace CRM_Repository.Service
{
    public class TaskGroup_Repository : ITaskGroup_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;

        public TaskGroup_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddTaskGroup(TaskGroupMaster Obj)
        {

            try
            {
                context.TaskGroupMasters.Add(Obj);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateTaskGroup(TaskGroupMaster Obj)
        {
            try
            {
                context.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TaskGroupMaster> GetAllTaskGroup()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM TaskGroupMaster with(nolock) WHERE IsActive = 1").ConvertToList<TaskGroupMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TaskGroupMaster GetTaskGroupById(int TaskGroupId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskGroupId", TaskGroupId);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskGroupMaster with(nolock) WHERE TaskGroupId=@TaskGroupId ", para).ConvertToList<TaskGroupMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<TaskGroupMaster> DuplicateEditTaskGroup(int TaskGroupId, string TaskGroupName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TaskGroupId", TaskGroupId);
                para[1] = new SqlParameter().CreateParameter("@TaskGroupName", TaskGroupName);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskGroupMaster with(nolock) WHERE RTRIM(LTRIM(TaskGroupName)) =RTRIM(LTRIM(@TaskGroupName))  AND TaskGroupId<>@TaskGroupId AND IsActive = 1", para).ConvertToList<TaskGroupMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<TaskGroupMaster> DuplicateTaskGroup(string TaskGroupName)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskGroupName", TaskGroupName);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskGroupMaster with(nolock) WHERE RTRIM(LTRIM(TaskGroupName)) =RTRIM(LTRIM(@TaskGroupName))  AND IsActive = 1", para).ConvertToList<TaskGroupMaster>().AsQueryable();

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
