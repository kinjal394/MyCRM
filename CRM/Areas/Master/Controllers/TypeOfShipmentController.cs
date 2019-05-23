using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class TypeOfShipmentController : Controller
    {
        // GET: Master/TypeOfShipment

        private TypeOfShipment_Repository _ITypeOfShipment_Repository;
        public TypeOfShipmentController()
        {
            this._ITypeOfShipment_Repository = new TypeOfShipment_Repository(new elaunch_crmEntities());

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTypeOfShipment()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTypeOfShipment(TypeOfShipmentMaster Data)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (Session["UserId"] != null)
                {
                    var Dub = _ITypeOfShipment_Repository.DuplicateTypeOfShipment(Data).ToList();
                    if (Dub.Count <= 0)
                    {
                        Data.IsActive = true;
                        _ITypeOfShipment_Repository.AddTypeOfShipment(Data);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Added successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Dublicate Shipment Type not allowed", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }

            }
            catch (Exception ex)
            {
                ex.SetLog("Create TypeOfShipment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTypeOfShipment(TypeOfShipmentMaster Data)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var Dub = _ITypeOfShipment_Repository.DuplicateTypeOfShipment(Data).ToList();
                if (Dub.Count <= 0)
                {
                    Data.IsActive = true;
                    _ITypeOfShipment_Repository.UpdateTypeOfShipment(Data);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Updated successfully", null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Dublicate Shipment Type not allowed", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Update TypeOfShipment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTypeOfShipment(int TypeOfShipmentId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
              
                _ITypeOfShipment_Repository.DeleteTypeOfShipment(TypeOfShipmentId);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Deleted successfully", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TypeOfShipment");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ITypeOfShipment_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}