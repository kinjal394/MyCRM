using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface ITaskType_Repository:IDisposable
    {
        void AddTaskType(TaskTypeMaster obj);
        void UpdateTaskType(TaskTypeMaster obj);
        TaskTypeMaster GetTaskTypeById(int id);
        IQueryable<TaskTypeMaster> GetAllTaskType();
        IQueryable<TaskTypeMaster> DuplicateTaskType(string TaskType);
        IQueryable<TaskTypeMaster> DuplicateEditTaskType(int TaskTypeId, string TaskType);
    }
}
