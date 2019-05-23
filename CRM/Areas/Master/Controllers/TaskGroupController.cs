using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class TaskGroupController : Controller
    {
        private ITaskGroup_Repository _ITaskGroup_Repository;

        public TaskGroupController()
        {
            this._ITaskGroup_Repository = new TaskGroup_Repository(new elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TaskGroupPopup()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveTaskGroup(TaskGroupMaster objTaskGroup)
        {
            DataResponse dataResponse = new DataResponse();

            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    TaskGroupMaster TaskGroupmaster = new TaskGroupMaster();
                    TaskGroupmaster.TaskGroupId = objTaskGroup.TaskGroupId;
                    TaskGroupmaster.TaskGroupName = objTaskGroup.TaskGroupName;
                    TaskGroupmaster.IsActive = true;
                    if (objTaskGroup.TaskGroupId > 0)
                    {
                        var celist = _ITaskGroup_Repository.DuplicateEditTaskGroup(TaskGroupmaster.TaskGroupId, TaskGroupmaster.TaskGroupName).ToList();
                        if (celist.Count == 0)
                        {
                            _ITaskGroup_Repository.UpdateTaskGroup(TaskGroupmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ITaskGroup_Repository.DuplicateTaskGroup(TaskGroupmaster.TaskGroupName).ToList();
                        if (clist.Count == 0)
                        {
                            _ITaskGroup_Repository.AddTaskGroup(TaskGroupmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

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
                ex.SetLog("Save TaskGroup");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult DeleteTaskGroup(string TaskGroupId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())   
                {
                    if (TaskGroupId != "")
                    {
                        int cid = Convert.ToInt32(TaskGroupId);
                        TaskGroupMaster Tmaster = new TaskGroupMaster();
                        Tmaster = _ITaskGroup_Repository.GetTaskGroupById(cid);
                        Tmaster.IsActive = false;
                        _ITaskGroup_Repository.UpdateTaskGroup(Tmaster);
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
                ex.SetLog("Delete TaskGroup");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetByIdLeger(int TaskGroupId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objTaskGroup = _ITaskGroup_Repository.GetTaskGroupById(TaskGroupId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objTaskGroup);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Leger");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ITaskGroup_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}