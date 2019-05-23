using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.ServiceContract;
using System.Data.Entity;
using CRM_Repository.Data;
using System.Data;
using System.Transactions;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;

using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class Task_Repository : ITask_Repository, IDisposable
    {
        dalc odal = new dalc();
        private elaunch_crmEntities context;
        public Task_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public TaskMaster EditTask(TaskMaster objtask)
        {
            try
            {
                context.TaskMasters.Add(objtask);
                context.SaveChanges();
                return objtask;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void InsertTask(TaskMaster objtask)
        {
            try
            {
                context.TaskMasters.Add(objtask);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateTask(TaskMaster objtask)
        {
            try
            {
                context.Entry(objtask).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public TaskMaster GetTaskId(int TaskId)
        {
            try
            {
                //TaskMaster tmaster = new TaskMaster();
                //return tmaster = context.TaskMasters.Find(TaskId);

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskId", TaskId);
                return new dalc().GetDataTable_Text("SELECT * FROM TaskMaster with(nolock) WHERE TaskId=@TaskId ", para).ConvertToList<TaskMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TaskModel> GetTaskInfromation(int UserId, int UserRollType)
        {
            try
            {
                string str = @"Select Distinct
                                        T.TaskId,T.Task,T.Priority,TP.PriorityName,T.Review,T.Status,TS.TaskStatus As TSTaskStatus,TS.TaskStatus,
                                        T.TaskTypeId,TTM.TaskType,T.GroupBy,T.IsActive,TDM.FromId,
                                        FromUM.Name as FromUser,
                                        case when TDM.NextFollowDate is NULL then '23:59:59' else TDM.NextFollowDate end As FollowDate,
                                        case when TDM.NextFollowTime is NULL then '1001/01/01' else TDM.NextFollowTime end As NextFollowTime,
                                        --TDM.FollowDate,TDM.NextFollowTime,
                                        TDM.Note,T.CreatedBy from
                                    TaskMaster AS T 
                                    LEFT JOIN gurjari_crmuser.TaskPriorityMaster AS TP ON T.Priority = TP.PriorityId  
                                    LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                                    LEFT JOIN gurjari_crmuser.TaskTypeMaster AS TTM ON T.TaskTypeId = TTM.TaskTypeId 
                                    LEFT JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.ToId = T.CreatedBy
                                    LEFT JOIN gurjari_crmuser.UserMaster as FromUM  on FromUM.UserId = TDM.FromId 
                                    LEFT JOIN gurjari_crmuser.UserMaster as ToUM  on ToUM.UserId = TDM.ToId
                                Where ISNULL(T.IsActive,0)=1 ";
                if (UserRollType > 1)
                {
                    str += " AND TDM.ToId = " + UserId;
                }
                return odal.selectbyquerydt(str).ConvertToList<TaskModel>().AsQueryable();

                //List<TaskModel> obj = new List<TaskModel>();
                //var sample = context.TaskMasters.ToList();
                //foreach (var item in sample)
                //{
                //    int tempAssign = (item.ModifyBy == 0 || item.ModifyBy == null) ? Convert.ToInt32(item.CreatedBy) : Convert.ToInt32(item.ModifyBy);
                //    obj.Add(new TaskModel()
                //    {
                //        TaskId = item.TaskId,
                //        Task = item.Task,
                //        Status = item.Status,
                //        //  AssignTo = item.AssignTo,
                //        //  AssignToUser = context.UserMasters.Where(z => z.UserId == item.AssignTo).FirstOrDefault().UserName,
                //        // AssignFrom = tempAssign,
                //        //AssignFromUser = context.UserMasters.Where(z => z.UserId == tempAssign).FirstOrDefault().UserName,
                //        //   FollowTime = item.FollowTime,
                //        //   FollowDate = item.FollowDate,
                //        IsActive = item.IsActive,
                //        Priority = item.Priority,
                //        //  PreviousIds = item.PreviousIds,
                //        //  Note= item.Note
                //    });
                //}
                //return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CompleteTaskStatus(int TaskId)
        {
            try
            {
                TaskMaster objtask = context.TaskMasters.Where(z => z.TaskId == TaskId).FirstOrDefault();
                objtask.Status = 2;
                context.Entry(objtask).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateUpdate(TaskModel objTaskModel)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    TaskMaster objTaskMaster = new TaskMaster();
                    if (objTaskModel.TaskId == 0)
                    {
                        #region "INSERT"
                        objTaskMaster.Task = objTaskModel.Task;
                        objTaskMaster.Priority = objTaskModel.Priority;
                        objTaskMaster.Duration = objTaskModel.Duration;
                        objTaskMaster.DeadlineDate = objTaskModel.DeadlineDate;
                        //objTaskMaster.GroupBy = objTaskModel.GroupBy;
                        if (objTaskModel.TaskGroupId > 0)
                        {
                            objTaskMaster.TaskGroupId = objTaskModel.TaskGroupId;
                        }
                        objTaskMaster.Status = 1;
                        objTaskMaster.IsActive = true;
                        objTaskMaster.CreatedBy = Convert.ToInt32(objTaskModel.ModifyBy);
                        objTaskMaster.CreatedDate = DateTime.Now;
                        objTaskMaster = EditTask(objTaskMaster);


                        TaskDetailMaster objTaskDetailMaster = new TaskDetailMaster();
                        objTaskDetailMaster.TaskId = objTaskMaster.TaskId;
                        objTaskDetailMaster.NextFollowDate = objTaskModel.NextFollowDate;
                        objTaskDetailMaster.NextFollowTime = objTaskModel.NextFollowTime;
                        objTaskDetailMaster.FromId = objTaskModel.ModifyBy;
                        objTaskDetailMaster.ToId = objTaskModel.ModifyBy;
                        objTaskDetailMaster.TaskStatus = 1;
                        objTaskDetailMaster.Note = "Self Assign";
                        objTaskDetailMaster.IsActive = true;
                        objTaskDetailMaster.CreatedDate = DateTime.Now;
                        objTaskDetailMaster = context.TaskDetailMasters.Add(objTaskDetailMaster);

                        resVal = 1;
                        #endregion
                    }
                    else
                    {
                        #region "Update"
                        objTaskMaster = context.TaskMasters.Where(z => z.TaskId == objTaskModel.TaskId).SingleOrDefault();
                        if (objTaskMaster != null)
                        {
                            //TaskId,Task,Priority,Review,Status,IsActive,TaskTypeId,GroupBy
                            //TaskDetailId, TaskId, FromId, ToId, FollowDate, FollowTime, IsActive, Note, TaskStatus
                            objTaskMaster.Task = objTaskModel.Task;
                            objTaskMaster.Priority = objTaskModel.Priority;
                            objTaskMaster.Duration = objTaskModel.Duration;
                            objTaskMaster.DeadlineDate = objTaskModel.DeadlineDate;
                            objTaskMaster.Review = objTaskModel.Review;
                            objTaskMaster.Status = objTaskModel.Status;
                            if (objTaskModel.TaskTypeId > 0)
                            {
                                objTaskMaster.TaskTypeId = objTaskModel.TaskTypeId;
                            }
                            if (objTaskModel.TaskGroupId > 0)
                            {
                                objTaskMaster.TaskGroupId = objTaskModel.TaskGroupId;
                            }

                            objTaskMaster.GroupBy = objTaskModel.GroupBy;
                            objTaskMaster.IsActive = true;
                            objTaskMaster.ModifyBy = objTaskModel.ModifyBy;
                            objTaskMaster.ModifyDate = DateTime.Now;
                            context.Entry(objTaskMaster).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();

                            //TaskDetailMaster objTM = context.TaskDetailMasters.Where(z => z.TaskId == objTaskModel.TaskId && z.ToId == objTaskMaster.CreatedBy).SingleOrDefault();
                            //objTM.IsActive = false;
                            //objTM.FollowDate = objTaskModel.FollowDate;
                            //objTM.FollowTime = objTaskModel.FollowTime;
                            //objTM.Note = objTaskModel.Note;
                            //context.Entry(objTM).State = System.Data.Entity.EntityState.Modified;
                            //if (objTaskModel.ToId != null)
                            //{
                            //    List<int> InputToIdList = objTaskModel.ToId.Split(',').Select(int.Parse).ToList();
                            //    List<int> StoredToIdList = context.TaskDetailMasters.Where(z => z.TaskId == objTaskModel.TaskId && z.FromId != z.ToId && z.FromId == objTaskMaster.CreatedBy).Select(z => z.ToId.Value).ToList();
                            //    List<int> diff = StoredToIdList.Except(InputToIdList).ToList<int>();
                            //    foreach (var item in diff)
                            //    {
                            //        TaskDetailMaster objTaskDetailMaster = context.TaskDetailMasters.Where(z => z.ToId == item && z.TaskId == objTaskModel.TaskId).SingleOrDefault();
                            //        if (objTaskDetailMaster != null)
                            //        {
                            //            context.TaskDetailMasters.Remove(objTaskDetailMaster);
                            //        }
                            //    }

                            //    foreach (var item in InputToIdList)
                            //    {
                            //        TaskDetailMaster objTaskDetailMaster = context.TaskDetailMasters.Where(z => z.ToId == item && z.TaskId == objTaskModel.TaskId).SingleOrDefault();
                            //        if (objTaskDetailMaster != null)
                            //        {
                            //            objTaskDetailMaster.FromId = objTaskMaster.CreatedBy;
                            //            objTaskDetailMaster.ToId = item;
                            //            objTaskDetailMaster.FollowDate = objTaskModel.FollowDate;
                            //            objTaskDetailMaster.FollowTime = objTaskModel.FollowTime;
                            //            objTaskDetailMaster.Note = objTaskModel.Note;
                            //            //objTaskDetailMaster.TaskStatus = objTaskModel.TaskStatus; // Some Problem TaskStatus By : Hitesh 
                            //            context.Entry(objTaskDetailMaster).State = System.Data.Entity.EntityState.Modified;
                            //        }
                            //        else
                            //        {
                            //            TaskDetailMaster tempTaskDetailMaster = new TaskDetailMaster();
                            //            tempTaskDetailMaster.TaskId = objTaskModel.TaskId;
                            //            tempTaskDetailMaster.FromId = objTaskMaster.CreatedBy;
                            //            tempTaskDetailMaster.ToId = item;
                            //            tempTaskDetailMaster.FollowDate = objTaskModel.FollowDate;
                            //            tempTaskDetailMaster.FollowTime = objTaskModel.FollowTime;
                            //            tempTaskDetailMaster.IsActive = true;
                            //            tempTaskDetailMaster.Note = objTaskModel.Note;
                            //            tempTaskDetailMaster.TaskStatus = 1;
                            //            context.TaskDetailMasters.Add(tempTaskDetailMaster);
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    var objTaskDetailMaster = context.TaskDetailMasters.Where(z => z.TaskId == objTaskModel.TaskId && z.FromId != z.ToId).ToList();
                            //    foreach (var item in objTaskDetailMaster)
                            //    {
                            //        TaskDetailMaster tempTaskDetailMaster = context.TaskDetailMasters.Where(z => z.TaskDetailId == item.TaskDetailId).SingleOrDefault();
                            //        context.TaskDetailMasters.Remove(tempTaskDetailMaster);
                            //    }
                            //}
                        }

                        resVal = 2;

                        #endregion
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
        }

        public void DeleteTask(int TaskId, int UserId)
        {
            try
            {
                TaskMaster objTaskMaster = context.TaskMasters.Where(z => z.TaskId == TaskId).SingleOrDefault();
                objTaskMaster.IsActive = false;
                objTaskMaster.DeletedBy = UserId;
                objTaskMaster.DeletedDate = DateTime.Now;
                context.Entry(objTaskMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public TaskModel GetTaskInfoById(int id, int UserId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@TaskId", id);
                para[1] = new SqlParameter().CreateParameter("@UserId", UserId);
                var strquery = @"Select Distinct
                                T.TaskId,T.Task,T.DeadlineDate,T.Priority,TP.PriorityName,T.Review,T.Status,TS.TaskStatus As TSTaskStatus,TS.TaskStatus,
                                T.TaskTypeId,TTM.TaskType,T.GroupBy,T.IsActive,T.TaskGroupId,TG.TaskGroupName,
                                TDM.FromId,T.CreatedBy,T.Duration,
                                FromUM.Name as FromUser,
                                (SELECT STUFF((
                                SELECT Distinct ',' + Convert(Nvarchar,InTD.ToId) + '|' + InUM.Name
                                + '|' + Case When InTD.ToId in (Select FromId from TaskDetailMaster Where TaskId=T.TaskId And FromId  <>@UserId ";
                strquery += @" ) THEN '1' ELSE '0' END
                                FROM TaskDetailMaster As InTD
                                INNER JOIN UserMaster As InUM  on InUM.UserId = InTD.ToId 
                                Where InTD.TaskId = T.TaskId And InTD.ToId <> InTD.FromId AND InTD.FromId =@UserId ";
                strquery += @" FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As ToId,
                                --TDM.NextFollowDate,TDM.NextFollowTime,TDM.Note
                                (Select top 1 NextFollowTime from TaskDetailMaster Where TaskId=T.TaskId And ToId =@UserId order by TaskDetailid desc ";
                strquery += @" ) As NextFollowTime , (Select top 1 NextFollowDate from TaskDetailMaster Where TaskId=T.TaskId And ToId =@UserId order by TaskDetailid desc ";
                strquery += @" ) As NextFollowDate , (Select top 1 Note from TaskDetailMaster Where TaskId=T.TaskId And ToId = @UserId order by TaskDetailid desc";
                strquery += @" ) As Note from 
                                TaskMaster AS T 
                                LEFT JOIN gurjari_crmuser.TaskGroupMaster AS TG ON T.TaskGroupId = TG.TaskGroupId 
                                LEFT JOIN gurjari_crmuser.TaskPriorityMaster AS TP ON T.Priority = TP.PriorityId 
                                LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                                LEFT JOIN gurjari_crmuser.TaskTypeMaster AS TTM ON T.TaskTypeId = TTM.TaskTypeId 
                                INNER JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId AND TDM.FromId =@UserId ";
                strquery += @" AND TDM.FromId<> TDM.ToId 
                                INNER JOIN gurjari_crmuser.UserMaster as FromUM  on FromUM.UserId = TDM.FromId
                                INNER JOIN gurjari_crmuser.UserMaster as ToUM  on ToUM.UserId = TDM.ToId
                                Where ISNULL(T.IsActive, 0)= 1 And T.TaskId =@TaskId ";
                return odal.GetDataTable_Text(strquery,para).ConvertToList<TaskModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public TaskDetailMaster GetTaskFollowUpById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Taskid", id);
                return new dalc().GetDataTable_Text(@"SELECT Top 1 TaskDetailId,TaskId,Note,NextFollowDate,NextFollowTime,B.TaskStatus AS StatusName,
                                                    A.TaskStatus As Status, FromId, A.IsActive, ToId,
                                                    A.PlanDateTime,cast(A.PlanDateTime as time) As PlanTime,A.ActualDate,A.ActualTime
                                                    FROM TaskDetailMaster AS A
                                                    INNER JOIN TaskStatusMaster AS B ON A.TaskStatus = B.StatusId WHERE TaskId =@Taskid Order by  TaskDetailId Desc", para).ConvertToList<TaskDetailMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskDetailMaster FetchTaskFollowUpById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskDetailId", id);
                return new dalc().GetDataTable_Text(@"SELECT TaskDetailId,TaskId,Note,NextFollowDate,NextFollowTime,B.TaskStatus AS StatusName,
                                                    A.TaskStatus As Status, FromId, A.IsActive, ToId,
                                                    A.PlanDateTime,cast(A.PlanDateTime as time) As PlanTime,A.ActualDate,A.ActualTime
                                                    FROM TaskDetailMaster AS A
                                                    INNER JOIN TaskStatusMaster AS B ON A.TaskStatus = B.StatusId WHERE TaskDetailId =@TaskDetailId", para).ConvertToList<TaskDetailMaster>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskModel GetTaskDatabyId(int TaskId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TaskId", TaskId);
                var strquery = @"Select Distinct
                                T.TaskId,T.Task,T.Priority,TP.PriorityName,T.Review,T.Status,TS.TaskStatus As TSTaskStatus,TS.TaskStatus,
                                T.TaskTypeId,TTM.TaskType,T.GroupBy,T.IsActive,TDM.FromId,
                                FromUM.Name as FromUser,
                                TDM.FollowDate,TDM.NextFollowTime,TDM.Note,T.CreatedBy
                                from
                                TaskMaster AS T 
                                LEFT JOIN gurjari_crmuser.TaskPriorityMaster AS TP ON T.Priority = TP.PriorityId  
                                LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                                LEFT JOIN gurjari_crmuser.TaskTypeMaster AS TTM ON T.TaskTypeId = TTM.TaskTypeId 
                                INNER JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.ToId = T.CreatedBy
                                INNER JOIN gurjari_crmuser.UserMaster as FromUM  on FromUM.UserId = TDM.FromId 
                                INNER JOIN gurjari_crmuser.UserMaster as ToUM  on ToUM.UserId = TDM.ToId 
                                Where T.TaskId =@TaskId AND ISNULL(T.IsActive,0)=1";
                return odal.GetDataTable_Text(strquery, para).ConvertToList<TaskModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public TaskDetailMaster InsertTaskDetail(TaskDetailMaster objtaskdetail)
        {
            try
            {
                context.TaskDetailMasters.Add(objtaskdetail);
                context.SaveChanges();
                return objtaskdetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateTaskDetail(TaskDetailMaster objtaskdetail)
        {
            try
            {
                objtaskdetail.CreatedDate = DateTime.Now;
                context.Entry(objtaskdetail).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
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
