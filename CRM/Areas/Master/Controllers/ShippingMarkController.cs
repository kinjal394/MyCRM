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
    public class ShippingMarkController : Controller
    {
        private IShippingMark_Repository _IShippingMark_Repository;
        public ShippingMarkController()
        {
            this._IShippingMark_Repository = new ShippingMark_Repository(new elaunch_crmEntities());

        }

        // GET: Master/ShippingMark
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddShippingMark()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveShippingMark(ShipmentMarkMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IShippingMark_Repository.CheckShippingMark(obj, false))
                    {
                        ShipmentMarkMaster shipObj = new ShipmentMarkMaster();
                        shipObj.BuyerName = obj.BuyerName;
                        shipObj.ShipperName = obj.ShipperName;
                        shipObj.ConsigneeName = obj.ConsigneeName;
                        shipObj.POL = obj.POL;
                        shipObj.POD = obj.POD;
                        shipObj.CreatedDate = DateTime.Now;
                        shipObj.CreatedBy = sessionUtils.UserId;
                        shipObj.ModifyDate = DateTime.Now;
                        shipObj.ModifyBy = sessionUtils.UserId;
                        shipObj.IsActive = true;
                        _IShippingMark_Repository.AddShippingMark(shipObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Shipping Mark " + MessageValue.Exists, null);
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
                ex.SetLog("Create/Update ShippingMark");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteShippingMark(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ShipmentMarkMaster shipObj = _IShippingMark_Repository.GetShippingMarkById(Convert.ToInt32(id));
                if (shipObj != null)
                {

                    shipObj.IsActive = false;
                    _IShippingMark_Repository.UpdateShippingMark(shipObj);
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
                ex.SetLog("Delete ShippingMark");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateShippingMark(ShipmentMarkMaster shipObj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            { 
                if (ModelState.IsValid)
                {
                    if (!_IShippingMark_Repository.CheckShippingMark(shipObj, true))
                    {
                        shipObj.CreatedDate = DateTime.Now;
                        shipObj.CreatedBy = sessionUtils.UserId;
                        shipObj.ModifyDate = DateTime.Now;
                        shipObj.ModifyBy = sessionUtils.UserId;
                        shipObj.IsActive = true;
                        _IShippingMark_Repository.UpdateShippingMark(shipObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Shipping Mark" + MessageValue.Exists, null);
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
                ex.SetLog("Update ShippingMark");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _IShippingMark_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}