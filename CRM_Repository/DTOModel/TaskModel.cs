using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class TaskDetailMaster
    {
        public string StatusName { get; set; }
        public int Status { get; set; }
        public TimeSpan PlanTime { get; set; }
    }

    public class TaskModel
    {
        public int TaskId { get; set; }
        public string Task { get; set; }
        public int Priority { get; set; }
        public string PriorityName { get; set; }
        public string Review { get; set; }
        public int Status { get; set; }
        public int TaskTypeId { get; set; }
        public string TaskType { get; set; }
        public string TaskStatus { get; set; }
        public string TSTaskStatus { get; set; }
        public int TaskGroupId { get; set; }
        public string TaskGroupName { get; set; }
        public string GroupBy { get; set; }
        public bool IsActive { get; set; }
        public int FromId { get; set; }
        public string FromUser { get; set; }
        public string ToId { get; set; }
        public string Duration { get; set; }
        public Nullable<System.DateTime> NextFollowDate { get; set; }
        public Nullable<System.TimeSpan> NextFollowTime { get; set; }
        public Nullable<System.DateTime> DeadlineDate { get; set; }

        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
    }

    //public  class TaskModel
    //{
    //    public int TaskId { get; set; }
    //    public string Task { get; set; }
    //    public int Status { get; set; }
    //    public int AssignTo { get; set; }
    //    public string AssignToUser { get; set; }
    //    public int AssignFrom { get; set; }
    //    public string AssignFromUser { get; set; }
    //    public Nullable<System.TimeSpan> FollowTime { get; set; }
    //    public Nullable<System.DateTime> FollowDate { get; set; }
    //    public bool IsActive { get; set; }
    //    public int Priority { get; set; }
    //    public string PreviousIds { get; set; }
    //    public string Note { get; set; }
    //}
}
