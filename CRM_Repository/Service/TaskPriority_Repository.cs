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
    public class TaskPriority_Repository : ITaskPriority_Repository,IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        public TaskPriority_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddTaskPriority(TaskPriorityMaster taskpriority)
        {
            try
            {
                context.TaskPriorityMasters.Add(taskpriority);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CheckTaskPriority(TaskPriorityMaster obj, bool isUpdate)
        {

            try
            {
                if (isUpdate)
                {
                     SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@PriorityName", obj.PriorityName);
                    para[1] = new SqlParameter().CreateParameter("@PriorityId", obj.PriorityId);
                    return new dalc().GetDataTable_Text("SELECT * FROM TaskPriorityMaster with(nolock) WHERE RTRIM(LTRIM(PriorityName)) = RTRIM(LTRIM(@PriorityName)) AND PriorityId<>@PriorityId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@PriorityName", obj.PriorityName);
                    return new dalc().GetDataTable_Text("SELECT * FROM TaskPriorityMaster with(nolock) WHERE RTRIM(LTRIM(PriorityName)) = RTRIM(LTRIM(@PriorityName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IQueryable<TaskPriorityMaster> GetAllTaskPriority()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM TaskPriorityMaster with(nolock) ").ConvertToList<TaskPriorityMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<TaskPriorityMaster> GetAllActiveTaskPriority()
        {
            try
            {
               return new dalc().selectbyquerydt("SELECT * FROM TaskPriorityMaster with(nolock) WHERE IsActive = 1").ConvertToList<TaskPriorityMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TaskPriorityMaster GetTaskPriorityById(int PriorityId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PriorityId", PriorityId);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskPriorityMaster with(nolock) WHERE PriorityId=@PriorityId AND IsActive = 1", para).ConvertToList<TaskPriorityMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateTaskPriority(TaskPriorityMaster taskpriority)
        {
            try
            {
                context.Entry(taskpriority).State = System.Data.Entity.EntityState.Modified;
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
