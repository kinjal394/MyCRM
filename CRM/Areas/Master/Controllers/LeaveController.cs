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
    public class LeaveController : Controller
    {

        private ILeave_Repository _ILeave_Repository;

        public LeaveController()
        {
            this._ILeave_Repository = new Leave_Repository(new elaunch_crmEntities());
        }

        // GET: Master/Leave
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageLeave()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveLeave(LeaveMaster objleave)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                 
                    if (objleave.LeaveId > 0)
                    {
                        LeaveMaster lmaster = _ILeave_Repository.GetLeaveID(objleave.LeaveId);
                        lmaster.UserId = sessionUtils.UserId;
                        lmaster.FromDate = objleave.FromDate;
                        lmaster.ToDate = objleave.ToDate;
                        lmaster.IsHalf = objleave.IsHalf;
                        lmaster.TotalDays = objleave.TotalDays;
                        lmaster.Reason = objleave.Reason;
                        lmaster.Status = objleave.Status;
                        lmaster.IsActive = true;
                        _ILeave_Repository.UpdateLeave(lmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                    else
                    {
                        LeaveMaster lmaster = new LeaveMaster();
                        lmaster.UserId = sessionUtils.UserId;
                        lmaster.FromDate = objleave.FromDate;
                        lmaster.ToDate = objleave.ToDate;
                        lmaster.IsHalf = objleave.IsHalf;
                        lmaster.TotalDays = objleave.TotalDays;
                        lmaster.Reason = objleave.Reason;
                        lmaster.Status = objleave.Status;
                        lmaster.IsActive = true;
                        _ILeave_Repository.InsertLeave(lmaster);
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
                ex.SetLog("Create/Update Leave");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateLeave(LeaveMaster objleave)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    LeaveMaster lmaster = new LeaveMaster();
                    //LeaveId,UserId,FromDate,ToDate,IsHalf,TotalDays,Reason,Status,Feedback,FeedbackFrom,IsActive
                    //lmaster.UserId = sessionUtils.UserId;
                    //lmaster.FromDate = objleave.FromDate;
                    //lmaster.ToDate = objleave.ToDate;
                    //lmaster.IsHalf = objleave.IsHalf;
                    //lmaster.TotalDays = objleave.TotalDays;
                    //lmaster.Reason = objleave.Reason;
                    lmaster.Status = objleave.Status;
                    lmaster.IsActive = true;
                    if (objleave.LeaveId > 0)
                    {
                        lmaster.Feedback = objleave.Feedback;
                        lmaster.FeedbackFrom = objleave.FeedbackFrom;
                        _ILeave_Repository.UpdateLeave(lmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Update Leave");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteLeave(int LeaveId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    LeaveMaster lmaster = new LeaveMaster();
                    lmaster = _ILeave_Repository.GetLeaveID(LeaveId);
                    lmaster.IsActive = false;
                    _ILeave_Repository.UpdateLeave(lmaster);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Leave");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdLeave(int LeaveId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objleave = _ILeave_Repository.GetLeaveID(LeaveId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objleave);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Leave by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ILeave_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}