using CRM.App_Start;
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
    [HasLoginSessionFilter]
    public class LicenseController : Controller
    {
        private ILicense_Repository _ILicense_Repository;
        public LicenseController()
        {
            this._ILicense_Repository = new License_Repository(new elaunch_crmEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddLicense()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveLicense(LicenseMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                LicenseMaster LicenseObj = new LicenseMaster();
                LicenseObj.LicenseId = d.LicenseId;
                LicenseObj.LicenseName = d.LicenseName;
                LicenseObj.IsActive = true;
                if(LicenseObj.LicenseId>0)
                {
                    var Liclist = _ILicense_Repository.DuplicateEditLicense(LicenseObj.LicenseId, LicenseObj.LicenseName).ToList();
                    if (Liclist.Count == 0)
                    {
                        _ILicense_Repository.UpdateLicense(LicenseObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "License Name Already Exists.", null);

                    }

                }
                else
                {
                    var Liclist = _ILicense_Repository.DuplicateLicense(LicenseObj.LicenseName).ToList();
                    if(Liclist.Count==0)
                    {
                        _ILicense_Repository.AddLicense(LicenseObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "License Name Already Exists.", null);

                    }

                }

            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update License");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLicense(int LicenseId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                LicenseMaster LicenseObj = new LicenseMaster();
                LicenseObj = _ILicense_Repository.GetLicenseByID(LicenseId);
                LicenseObj.IsActive = false;
                _ILicense_Repository.UpdateLicense(LicenseObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete License");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdLicense(int LicenseId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _ILicense_Repository.GetLicenseByID(Convert.ToInt32(LicenseId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get License By Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);

            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ILicense_Repository.Dispose();
            base.Dispose(disposing);
        }
    }

   
}