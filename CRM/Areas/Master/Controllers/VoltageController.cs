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
    public class VoltageController : Controller
    {
        private IVoltage_Repository _IVoltage_Repository;

        public VoltageController()
        {
            this._IVoltage_Repository = new Voltage_Repository(new elaunch_crmEntities());
        }
        // GET: Master/Voltage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddVoltage()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveVoltage(VoltageMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                VoltageMaster VoltageObj = new VoltageMaster();
                VoltageObj.VoltageId = d.VoltageId;
                VoltageObj.Voltage = d.Voltage.Trim();
                VoltageObj.IsActive = true;
                if (VoltageObj.VoltageId > 0)
                {
                    var cntList = _IVoltage_Repository.DuplicateEditVoltage(VoltageObj.VoltageId, VoltageObj.Voltage).ToList();
                    if (cntList.Count == 0)
                    {
                        _IVoltage_Repository.UpdateVoltage(VoltageObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Voltage Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IVoltage_Repository.DuplicateVoltage(VoltageObj.Voltage).ToList();
                    if (cntList.Count == 0)
                    {
                        _IVoltage_Repository.AddVoltage(VoltageObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Voltage Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Voltage");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteVoltage(int VoltageId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                VoltageMaster VoltageObj = new VoltageMaster();
                VoltageObj = _IVoltage_Repository.GetVoltageByID(VoltageId);
                VoltageObj.IsActive = false;
                _IVoltage_Repository.UpdateVoltage(VoltageObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Voltage");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdVoltage(int VoltageId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IVoltage_Repository.GetVoltageByID(Convert.ToInt32(VoltageId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Voltage by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IVoltage_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}