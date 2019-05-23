using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using System.Globalization;
using CRM_Repository.DTOModel;
using CRM.App_Start;

namespace CRM.Areas.Transaction.Controllers
{
    [HasLoginSessionFilter]
    public class TaskController : Controller
    {
        private ITask_Repository _ITask_Repository;
        private IUser_Repository _IUser_Repository;
        private ITaskPriority_Repository _ITaskPriority_Repository;
        private ITaskStatus_Repository _ITaskStatus_Repository;
        private IInquiry_Repository _IInquiry_Repository;
        public TaskController()
        {
            this._ITask_Repository = new Task_Repository(new elaunch_crmEntities());
            this._IUser_Repository = new User_Repository(new elaunch_crmEntities());
            this._ITaskPriority_Repository = new TaskPriority_Repository(new elaunch_crmEntities());
            this._ITaskStatus_Repository = new TaskStatus_Repository(new elaunch_crmEntities());
            this._IInquiry_Repository = new Inquiry_Repository(new elaunch_crmEntities());
        }

        // GET: Transaction/Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskPopup()
        {
            return View();
        }

        public ActionResult TaskFollowupList()
        {
            return View();
        }

        public ActionResult TaskFollowup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTask(TaskMaster objtask)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (objtask.TaskId > 0)
                    {
                        // Update
                        TaskMaster tmaster = _ITask_Repository.GetTaskId(objtask.TaskId);
                        tmaster.TaskId = objtask.TaskId;
                        tmaster.Task = objtask.Task;
                        tmaster.ModifyBy = sessionUtils.UserId;
                        tmaster.ModifyDate = DateTime.Now;
                        tmaster.Priority = objtask.Priority;
                        tmaster.Status = objtask.Status;
                        tmaster.DeadlineDate = objtask.DeadlineDate;
                        tmaster.IsActive = true;
                        _ITask_Repository.UpdateTask(tmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                    else
                    {
                        // Insert
                        TaskMaster tmaster = new TaskMaster();
                        tmaster.Task = objtask.Task;
                        tmaster.Priority = objtask.Priority;
                        tmaster.Status = 0;
                        tmaster.IsActive = true;
                        tmaster.CreatedBy = sessionUtils.UserId;
                        tmaster.CreatedDate = DateTime.Now;
                        tmaster.DeadlineDate = objtask.DeadlineDate;
                        _ITask_Repository.InsertTask(tmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Save Task");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteTask(string TaskId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (TaskId != "")
                    {
                        _ITask_Repository.DeleteTask(Convert.ToInt32(TaskId), sessionUtils.UserId);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Task");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReportingUserBind(int TaskId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                //if (sessionUtils.UserRollType > 1)
                //{
                //if (data.Count == 0)
                //{
                var data = _IUser_Repository.getAllUser().Where(x => x.UserId != sessionUtils.UserId).ToList();
                //}
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                //}
                //else
                //{
                //    TaskMaster task = _ITask_Repository.GetTaskId(TaskId);
                //    if (task != null)
                //    {
                //        var data = _IUser_Repository.GetReportingUser(task.CreatedBy).ToList();
                //        if (data.Count == 0)
                //        {
                //            data = _IUser_Repository.getAllUser().Where(x => x.UserId != sessionUtils.UserId).ToList();//.Where(x=> x.UserId != sessionUtils.UserId)
                //        }
                //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                //    }
                //    else
                //    {
                //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", null);
                //    }
                //}
                //var data = _IUser_Repository.GetReportingUser(sessionUtils.UserId).ToList();
                //dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All User in Task");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReportingUserBindTask(int TaskId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IUser_Repository.getAllUser().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All User in Task");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaskFollowUp(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ITask_Repository.GetTaskFollowUpById(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry FollowUp");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaskFollowUpByID(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ITask_Repository.FetchTaskFollowUpById(id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Inquiry FollowUp");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTaskInfromation()
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<TaskModel> objsample = _ITask_Repository.GetTaskInfromation(sessionUtils.UserId, sessionUtils.UserRollType).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objsample);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get TaskInfo");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CompleteTaskStatus(int TaskId)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _ITask_Repository.CompleteTaskStatus(TaskId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Task complete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get TaskStatus");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AssignNewUser(int TaskId, int UserId)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    TaskMaster objTaskMaster = _ITask_Repository.GetTaskId(TaskId);
                    objTaskMaster.ModifyBy = UserId;
                    objTaskMaster.ModifyDate = DateTime.Now;
                    //  objTaskMaster.PreviousIds = string.IsNullOrEmpty(objTaskMaster.PreviousIds) ? sessionUtils.UserId.ToString() : objTaskMaster.PreviousIds.ToString() + "|" + sessionUtils.UserId.ToString();
                    _ITask_Repository.UpdateTask(objTaskMaster);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Task assign successfully", null);

                }
                catch (Exception ex)
                {
                    ex.SetLog("Assign newTask");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditDashboardTask(TaskMaster objTaskMaster)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    TaskMaster TaskMasterInfo = _ITask_Repository.GetTaskId(objTaskMaster.TaskId);
                    //TaskMasterInfo.AssignTo = objTaskMaster.AssignTo;
                    //TaskMasterInfo.FollowDate = objTaskMaster.FollowDate;
                    //TaskMasterInfo.FollowTime = objTaskMaster.FollowTime;
                    TaskMasterInfo.Review = objTaskMaster.Review;
                    TaskMasterInfo.Status = objTaskMaster.Status;
                    TaskMasterInfo.ModifyBy = sessionUtils.UserId;
                    TaskMasterInfo.ModifyDate = DateTime.Now;
                    // TaskMasterInfo.PreviousIds = string.IsNullOrEmpty(TaskMasterInfo.PreviousIds) ? sessionUtils.UserId.ToString() : TaskMasterInfo.PreviousIds.ToString() + "|" + sessionUtils.UserId.ToString();
                    _ITask_Repository.UpdateTask(TaskMasterInfo);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Task assign successfully", null);

                }
                catch (Exception ex)
                {
                    ex.SetLog("Update Task");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdateTask(TaskModel objTaskModel)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    if (sessionUtils.UserRollType != 1)
                    {
                        objTaskModel.CreatedBy = sessionUtils.UserId;
                    }
                    objTaskModel.ModifyBy = sessionUtils.UserId;
                    if (objTaskModel.NextFollowDate == null)
                    {
                        objTaskModel.NextFollowDate = DateTime.Now.Date;
                    }
                    if (objTaskModel.NextFollowTime == null)
                    {
                        objTaskModel.NextFollowTime = DateTime.Now.TimeOfDay;
                    }
                   
                    int ResponseVal = _ITask_Repository.CreateUpdate(objTaskModel);
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                    else if (ResponseVal == 3)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Invalid User Update Record", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create Task");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, MessageValue.InvalidUser, null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveTaskFollowUp(TaskDetailMaster objTask)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    var response = new TaskDetailMaster();
                    objTask.FromId = sessionUtils.UserId;
                    //if (objTask.NextFollowDate == null)
                    //{
                    //    objTask.NextFollowDate = DateTime.Now.Date;
                    //}
                    //if (objTask.NextFollowTime == null)
                    //{
                    //    objTask.NextFollowTime = DateTime.Now.TimeOfDay;
                    //}
                    objTask.IsActive = true;
                    if (objTask.TaskDetailId > 0)
                    {
                        var data = _ITask_Repository.FetchTaskFollowUpById(objTask.TaskDetailId);
                        _ITask_Repository.UpdateTaskDetail(objTask);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Data Updated Successfully", null);

                    }
                    else
                    {
                        response = _ITask_Repository.InsertTaskDetail(objTask);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Data Insert Successfully", null);

                    }
                    Hubs.ChatHub.SendNotification(objTask.ToId.ToString(),Session["UserName"]+" follow task");
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Inquiry");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, MessageValue.InvalidUser, null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaskInfoById(int TaskId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TaskMaster task = _ITask_Repository.GetTaskId(TaskId);
                if (sessionUtils.UserRollType > 1)
                {
                    TaskModel data = _ITask_Repository.GetTaskInfoById(TaskId, sessionUtils.UserId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                else
                {
                    TaskModel data = _ITask_Repository.GetTaskInfoById(TaskId, task.CreatedBy);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Task by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaskDatabyId(int TaskId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TaskModel data = _ITask_Repository.GetTaskDatabyId(TaskId);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Task");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}