using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface ITaskGroup_Repository:IDisposable
    {
        void AddTaskGroup(TaskGroupMaster obj);


        void UpdateTaskGroup(TaskGroupMaster obj);




        IQueryable<TaskGroupMaster> GetAllTaskGroup();
        IQueryable<TaskGroupMaster> DuplicateTaskGroup(string TaskGroupName);
        TaskGroupMaster GetTaskGroupById(int TaskGroupId);
        IQueryable<TaskGroupMaster> DuplicateEditTaskGroup(int TaskGroupId, string TaskGroupName);
    }
}
