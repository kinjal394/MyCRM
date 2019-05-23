using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class ModeShipmentController : Controller
    {
        private IModeShipment_Repository _IModeShipment_Repository;
        // GET: Master/ModeShipment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ModeShipmentPopup()
        {
            return View();
        }
        public ModeShipmentController()
        {
            this._IModeShipment_Repository = new ModeShipment_Repository(new elaunch_crmEntities());

        }

        [HttpPost]
        public JsonResult SaveModeShipment(ShipmentMaster objship)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    ShipmentMaster ship = new ShipmentMaster();
                    ship.ShipmentId = objship.ShipmentId;
                    ship.ModeOfShipment = objship.ModeOfShipment;
                    ship.IsActive = true;
                    if (objship.ShipmentId > 0)
                    {
                        var plist = _IModeShipment_Repository.DuplicateEditShip(ship.ShipmentId, ship.ModeOfShipment).ToList();
                        if (plist.Count == 0)
                        {
                            _IModeShipment_Repository.UpdateShip(ship);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                            //msg = "Success";
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "ModeShipment Already Exists", null);
                            //msg = "Already Exists";
                        }
                        //_IModeShipment_Repository.UpdateShip(ship);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IModeShipment_Repository.DuplicateShip(ship.ModeOfShipment).ToList();
                        if (clist.Count == 0)
                        {
                            _IModeShipment_Repository.AddShip(ship);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "ModeShipment Already Exists", null);

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
                ex.SetLog("Create/Update ModeShipment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
            //try
            //{
            //    ShipmentMaster ship = new ShipmentMaster();
            //    ship.ModeOfShipment = obj.ModeOfShipment;
            //    ship.IsActive = true;
            //    _IModeShipment_Repository.AddShip(ship);
            //    return View();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }

        [HttpPost]
        public JsonResult DeleteShip(string ShipmentId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (ShipmentId != "")
                    {
                        int sid = Convert.ToInt32(ShipmentId);
                        ShipmentMaster cmaster = new ShipmentMaster();
                        cmaster = _IModeShipment_Repository.GetShipById(sid);
                        cmaster.IsActive = false;
                        _IModeShipment_Repository.UpdateShip(cmaster);
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
                ex.SetLog("Delete ModeShipment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetShipById(int ShipmentId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objship = _IModeShipment_Repository.GetShipById(ShipmentId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objship);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get ModeShipment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
            //return Json(objship, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IModeShipment_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}