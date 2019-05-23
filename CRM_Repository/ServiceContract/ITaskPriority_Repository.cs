using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ITaskPriority_Repository : IDisposable
    {
        void AddTaskPriority(TaskPriorityMaster taskpriority);
        void UpdateTaskPriority(TaskPriorityMaster taskpriority);
        TaskPriorityMaster GetTaskPriorityById(int id);
        IQueryable<TaskPriorityMaster> GetAllTaskPriority();
        IQueryable<TaskPriorityMaster> GetAllActiveTaskPriority();
        bool CheckTaskPriority(TaskPriorityMaster obj, bool isUpdate); 
    }
}
