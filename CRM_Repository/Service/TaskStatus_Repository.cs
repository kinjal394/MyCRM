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
    public class TaskStatus_Repository : ITaskStatus_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public TaskStatus_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddTaskStatus(TaskStatusMaster taskstatus)
        {
            try
            {
                context.TaskStatusMasters.Add(taskstatus);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CheckTaskStatus(TaskStatusMaster obj, bool isUpdate)
        {

            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@TaskStatus", obj.TaskStatus);
                    para[1] = new SqlParameter().CreateParameter("@StatusId", obj.StatusId);
                    return new dalc().GetDataTable_Text("SELECT * FROM TaskStatusMaster with(nolock) WHERE RTRIM(LTRIM(TaskStatus)) = RTRIM(LTRIM(@TaskStatus)) AND StatusId<>@StatusId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@TaskStatus", obj.TaskStatus);
                    return new dalc().GetDataTable_Text("SELECT * FROM TaskStatusMaster with(nolock) WHERE RTRIM(LTRIM(TaskStatus)) = RTRIM(LTRIM(@TaskStatus)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IQueryable<TaskStatusMaster> GetAllTaskStatus()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM TaskStatusMaster with(nolock) ").ConvertToList<TaskStatusMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<TaskStatusMaster> GetAllActiveTaskStatus()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM TaskStatusMaster with(nolock) WHERE IsActive = 1").ConvertToList<TaskStatusMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TaskStatusMaster GetTaskStatusById(int StatusId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@StatusId", StatusId);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskStatusMaster with(nolock) WHERE StatusId=@StatusId ", para).ConvertToList<TaskStatusMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateTaskStatus(TaskStatusMaster taskstatus)
        {
            try
            {
                context.Entry(taskstatus).State = System.Data.Entity.EntityState.Modified;
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
