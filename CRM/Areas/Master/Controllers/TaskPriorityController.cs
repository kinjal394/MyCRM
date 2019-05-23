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
    public class TaskPriorityController : Controller
    {
        private ITaskPriority_Repository _ITaskPriority_Repository;
        public TaskPriorityController()
        {
            this._ITaskPriority_Repository = new TaskPriority_Repository(new elaunch_crmEntities());

        }
        // GET: Master/TaskPriority
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTaskPriority()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTaskPriority(TaskPriorityMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_ITaskPriority_Repository.CheckTaskPriority(obj, false))
                    {
                        TaskPriorityMaster taskpriorityObj = new TaskPriorityMaster();
                        taskpriorityObj.PriorityName = obj.PriorityName;
                        taskpriorityObj.IsActive = true;
                        _ITaskPriority_Repository.AddTaskPriority(taskpriorityObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Task Priority" + MessageValue.Exists, null);
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
                ex.SetLog("Create/Update TaskPriority");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            } 
        }

        [HttpPost]
        public JsonResult DeleteTaskPriority(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TaskPriorityMaster taskpriorityObj = _ITaskPriority_Repository.GetTaskPriorityById(Convert.ToInt32(id));
                if (taskpriorityObj != null)
                {
                    taskpriorityObj.IsActive = false;
                    _ITaskPriority_Repository.UpdateTaskPriority(taskpriorityObj);
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
                ex.SetLog("Delete TaskPriority");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateTaskPriority(TaskPriorityMaster tasktriorityObj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_ITaskPriority_Repository.CheckTaskPriority(tasktriorityObj, true))
                    {
                        tasktriorityObj.IsActive = true;
                        _ITaskPriority_Repository.UpdateTaskPriority(tasktriorityObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Task Priority" + MessageValue.Exists, null);
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
                ex.SetLog("Update TaskPriority");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _ITaskPriority_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}