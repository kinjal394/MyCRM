using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.App_Start;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class ApplicableChargeController : Controller
    {
        private IApplicableCharge_Repository _IApplicableCharge_Repository;
        public ApplicableChargeController()
        {
            this._IApplicableCharge_Repository = new ApplicableCharge_Repository(new elaunch_crmEntities());
        }
        // GET: Master/ApplicableCharge
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddApplicableChar()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveApplicableChar(ApplicableChargeMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ApplicableChargeMaster AppliCharObj = new ApplicableChargeMaster();
                AppliCharObj.ApplicableChargeId = d.ApplicableChargeId;
                AppliCharObj.ApplicableChargeName = d.ApplicableChargeName.Trim();
                AppliCharObj.IsActive = true;
                if (AppliCharObj.ApplicableChargeId > 0)
                {
                    var cntList = _IApplicableCharge_Repository.DuplicateEditApplicableChargeName(AppliCharObj.ApplicableChargeId, AppliCharObj.ApplicableChargeName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IApplicableCharge_Repository.UpdateAppliChar(AppliCharObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "ApplicableCharge Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IApplicableCharge_Repository.DuplicateApplicableChargeName(AppliCharObj.ApplicableChargeName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IApplicableCharge_Repository.AddAppliChar(AppliCharObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "ApplicableCharge Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Applicable Charge");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdApplicableCharge(int ApplicableChargeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IApplicableCharge_Repository.getApplicharbyId(Convert.ToInt32(ApplicableChargeId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get ApplicableCharge by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteApplicableChar(int ApplicableChargeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                ApplicableChargeMaster applicharObj = new ApplicableChargeMaster();
                applicharObj = _IApplicableCharge_Repository.getApplicharbyId(ApplicableChargeId);
                applicharObj.IsActive = false;
                _IApplicableCharge_Repository.UpdateAppliChar(applicharObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Applicable Charge");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IApplicableCharge_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}