using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;

namespace CRM.Areas.Master.Controllers
{
    public class SalaryHeadController : Controller
    {
        private ISalaryHead_Repository _ISalaryHead_Repository;
        public SalaryHeadController()
        {
            this._ISalaryHead_Repository = new SalaryHead_Repository(new elaunch_crmEntities());
        }
        // GET: Master/SalaryHead
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSalaryHead()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveSalaryHead(SalaryHeadMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                SalaryHeadMaster SalaryHeadObj = new SalaryHeadMaster();
                SalaryHeadObj.SalaryHeadId = obj.SalaryHeadId;
                SalaryHeadObj.SalaryHeadName = obj.SalaryHeadName.Trim();
                SalaryHeadObj.IsActive = true;
                if (SalaryHeadObj.SalaryHeadId > 0)
                {
                    var cntList = _ISalaryHead_Repository.DuplicateEditSalaryHead(SalaryHeadObj.SalaryHeadId, SalaryHeadObj.SalaryHeadName).ToList();
                    if (cntList.Count == 0)
                    {
                        _ISalaryHead_Repository.UpdateSalaryHead(SalaryHeadObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "SalaryHead Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _ISalaryHead_Repository.DuplicateSalaryHead(SalaryHeadObj.SalaryHeadName).ToList();
                    if (cntList.Count == 0)
                    {
                        _ISalaryHead_Repository.AddSalaryHead(SalaryHeadObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "SalaryHead Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Salary Head");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSalaryHead(int SalaryHeadId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                SalaryHeadMaster SalaryHeadObj = new SalaryHeadMaster();
                SalaryHeadObj = _ISalaryHead_Repository.GetSalaryHeadById(SalaryHeadId);
                SalaryHeadObj.IsActive = false;
                _ISalaryHead_Repository.UpdateSalaryHead(SalaryHeadObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete SalaryHead");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdSalaryHead(int SalaryHeadId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _ISalaryHead_Repository.GetSalaryHeadById(Convert.ToInt32(SalaryHeadId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get SalaryHead by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ISalaryHead_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}