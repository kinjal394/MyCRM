using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ITaskStatus_Repository : IDisposable
    {
        void AddTaskStatus(TaskStatusMaster taskstatus);
        void UpdateTaskStatus(TaskStatusMaster taskstatus);
        TaskStatusMaster GetTaskStatusById(int id);
        IQueryable<TaskStatusMaster> GetAllTaskStatus();
        IQueryable<TaskStatusMaster> GetAllActiveTaskStatus();
        bool CheckTaskStatus(TaskStatusMaster obj, bool isUpdate);
    }
}
