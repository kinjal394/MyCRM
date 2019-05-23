using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface ITask_Repository : IDisposable
    {
        void InsertTask(TaskMaster objtask);
        TaskMaster EditTask(TaskMaster objtask);
        void UpdateTask(TaskMaster objtask);
        IQueryable<TaskModel> GetTaskInfromation(int UserId, int UserRollType);
        void CompleteTaskStatus(int TaskId);
        TaskMaster GetTaskId(int TaskId);
        int CreateUpdate(TaskModel objTaskModel);
        void DeleteTask(int TaskId, int UserId);
        TaskModel GetTaskInfoById(int id, int UserId);
        TaskModel GetTaskDatabyId(int id);
        TaskDetailMaster GetTaskFollowUpById(int id);
        TaskDetailMaster FetchTaskFollowUpById(int id);
        TaskDetailMaster InsertTaskDetail(TaskDetailMaster objtaskdetail);
        void UpdateTaskDetail(TaskDetailMaster objtaskdetail);
    }
}
