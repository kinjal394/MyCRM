using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class TaskStatusController : Controller
    {
        private ITaskStatus_Repository _ITaskStatus_Repository;
        public TaskStatusController()
        {
            this._ITaskStatus_Repository = new TaskStatus_Repository(new elaunch_crmEntities());

        }
        // GET: Master/TaskStatus
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTaskStatus()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTaskStatus(TaskStatusMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_ITaskStatus_Repository.CheckTaskStatus(obj, false))
                    {
                        TaskStatusMaster taskpriorityObj = new TaskStatusMaster();
                        taskpriorityObj.TaskStatus = obj.TaskStatus;
                        taskpriorityObj.IsActive = true;
                        _ITaskStatus_Repository.AddTaskStatus(taskpriorityObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound,"Task Status"+ MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update TaskStatus");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteTaskStatus(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TaskStatusMaster taskpriorityObj = _ITaskStatus_Repository.GetTaskStatusById(Convert.ToInt32(id));
                if (taskpriorityObj != null)
                {
                    taskpriorityObj.IsActive = false;
                    _ITaskStatus_Repository.UpdateTaskStatus(taskpriorityObj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TaskStatus");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateTaskStatus(TaskStatusMaster tasktriorityObj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_ITaskStatus_Repository.CheckTaskStatus(tasktriorityObj, true))
                    {
                        tasktriorityObj.IsActive = true;
                        _ITaskStatus_Repository.UpdateTaskStatus(tasktriorityObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Task Status" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.SetLog("Update TaskStatus");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult GetAllTaskStatus() {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<TaskStatusMaster> objTaskStatus = _ITaskStatus_Repository.GetAllTaskStatus().ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objTaskStatus);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All TaskStatus");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ITaskStatus_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}