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
    public class PortController : Controller
    {
        private IPort_Repository _IPort_Repository;
        // GET: Master/Port
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PortPopup()
        {
            return View();
        }
        public PortController()

        {
            this._IPort_Repository = new Port_Repository(new elaunch_crmEntities());

        }

        [HttpPost]
        public JsonResult SavePort(PortMaster objport)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    PortMaster pmaster = new PortMaster();
                    pmaster.PortId = objport.PortId;
                    pmaster.PortName = objport.PortName;
                    pmaster.CountryId = objport.CountryId;
                    pmaster.IsActive = true;
                    if (pmaster.PortId > 0)
                    {
                        var plist = _IPort_Repository.DuplicateEditPort(pmaster.PortId, pmaster.PortName).ToList();
                        if (plist.Count == 0)
                        {
                            _IPort_Repository.UpdatePort(pmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Port Already Exists", null);

                        }
                    }
                    else
                    {
                        var plist = _IPort_Repository.DuplicatePort(pmaster.PortName).ToList();
                        if (plist.Count == 0)
                        {
                            _IPort_Repository.AddPort(pmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Port Already Exists", null);

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
                ex.SetLog("Create/Update Port");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePort(string PortId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (PortId != "")
                    {
                        int pid = Convert.ToInt32(PortId);
                        PortMaster pmaster = new PortMaster();
                        pmaster = _IPort_Repository.GetPortById(pid);
                        pmaster.IsActive = false;
                        _IPort_Repository.UpdatePort(pmaster);
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
                ex.SetLog("Delete Port");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdPort(int PortID)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objport = _IPort_Repository.GetPortById(PortID);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objport);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Port");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IPort_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}