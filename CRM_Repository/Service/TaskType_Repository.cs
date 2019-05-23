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
    public class TaskType_Repository : ITaskType_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public TaskType_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void AddTaskType(TaskTypeMaster obj)
        {
            try
            {
                context.TaskTypeMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public TaskTypeMaster GetTaskTypeById(int id)
        {
            try
            {
                // return context.TaskTypeMasters.Find(id);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskTypeId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskTypeMaster with(nolock) WHERE TaskTypeId=@TaskTypeId ", para).ConvertToList<TaskTypeMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IQueryable<TaskTypeMaster> DuplicateEditTaskType(int TaskTypeId, string TaskType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TaskType", TaskType);
                para[1] = new SqlParameter().CreateParameter("@TaskTypeId", TaskTypeId);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskTypeMaster with(nolock) WHERE RTRIM(LTRIM(TaskType))=RTRIM(LTRIM(@TaskType)) AND TaskTypeId<>@TaskTypeId AND IsActive = 1", para).ConvertToList<TaskTypeMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<TaskTypeMaster> DuplicateTaskType(string TaskType)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskType", TaskType);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskTypeMaster with(nolock) WHERE RTRIM(LTRIM(TaskType))=RTRIM(LTRIM(@TaskType))  AND IsActive = 1", para).ConvertToList<TaskTypeMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateTaskType(TaskTypeMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TaskTypeMaster> GetAllTaskType()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM TaskTypeMaster with(nolock) WHERE IsActive = 1").ConvertToList<TaskTypeMaster>().AsQueryable();
            }
            catch
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
