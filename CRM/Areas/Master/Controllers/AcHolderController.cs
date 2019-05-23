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
    public class AcHolderController : Controller
    {
        private IAcHolder_Repository _IIAcHolder_Repository;

        public AcHolderController()
        {
            this._IIAcHolder_Repository = new AcHolder_Repository(new elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAcHolder()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveAcHolder(AcHolderMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                AcHolderMaster AcHolderObj = new AcHolderMaster();
                AcHolderObj.AcHolderCode = d.AcHolderCode;
                AcHolderObj.AcHolderName = d.AcHolderName.Trim();
                AcHolderObj.IsActive = true;
                if (AcHolderObj.AcHolderCode > 0)
                {
                    var cntList = _IIAcHolder_Repository.DuplicateEditAcHolder(AcHolderObj.AcHolderCode, AcHolderObj.AcHolderName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IIAcHolder_Repository.UpdateAcHolder(AcHolderObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "AcHolder Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IIAcHolder_Repository.DuplicateAcHolder(AcHolderObj.AcHolderName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IIAcHolder_Repository.AddAcHolder(AcHolderObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "AcHolder Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update AcHolder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAcHolder(int AcHolderCode)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                AcHolderMaster AcHolderObj = new AcHolderMaster();
                AcHolderObj = _IIAcHolder_Repository.GetAcHolderByID(AcHolderCode);
                AcHolderObj.IsActive = false;
                _IIAcHolder_Repository.UpdateAcHolder(AcHolderObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete AcHolder");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdAcHolder(int AcHolderCode)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IIAcHolder_Repository.GetAcHolderByID(Convert.ToInt32(AcHolderCode));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get AcHolder by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IIAcHolder_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}