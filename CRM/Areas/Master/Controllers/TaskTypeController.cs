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
    public class TaskTypeController : Controller
    {
        private ITaskType_Repository _ITaskType_Repository;
        // GET: Master/TaskType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TaskTypePopup()
        {
            return View();
        }
        public TaskTypeController()

        {
            this._ITaskType_Repository = new TaskType_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SaveTaskType(TaskTypeMaster objTaskType)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    TaskTypeMaster task = new TaskTypeMaster();
                    task = objTaskType;
                    task.IsActive = true;
                    if (objTaskType.TaskTypeId > 0)
                    {
                        var plist = _ITaskType_Repository.DuplicateEditTaskType(task.TaskTypeId, task.TaskType).ToList();
                        if (plist.Count == 0)
                        {
                            _ITaskType_Repository.UpdateTaskType(task);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "TaskType Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ITaskType_Repository.DuplicateTaskType(task.TaskType).ToList();
                        if (clist.Count == 0)
                        {
                            _ITaskType_Repository.AddTaskType(task);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "TaskType Already Exists", null);

                            // Already Exists
                        }
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update TaskType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteTaskType(string TaskTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (TaskTypeId != "")
                    {
                        int cid = Convert.ToInt32(TaskTypeId);
                        TaskTypeMaster tmaster = new TaskTypeMaster();
                        tmaster = _ITaskType_Repository.GetTaskTypeById(cid);
                        tmaster.IsActive = false;
                        _ITaskType_Repository.UpdateTaskType(tmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TaskType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTaskTypeById(int TaskTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objtasktype = _ITaskType_Repository.GetTaskTypeById(TaskTypeId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objtasktype);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get TaskType by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ITaskType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}