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
    public class DeliveryTermsController : Controller
    {
        private IDeliveryTerms_Repository _IDeliveryTerms_Repository;

        public DeliveryTermsController()
        {
            this._IDeliveryTerms_Repository = new DeliveryTerms_Repository(new elaunch_crmEntities());
        }

        // GET: Master/DeliveryTerms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDeliveryTerms()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDeliveryTerms(DeliveryTermsMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                DeliveryTermsMaster deliveryObj = new DeliveryTermsMaster();
                deliveryObj.TermsId = d.TermsId;
                deliveryObj.DeliveryName = d.DeliveryName.Trim();
                deliveryObj.Description = d.Description.Trim();
                deliveryObj.IsActive = true;
                if (deliveryObj.TermsId > 0)
                {
                    var cntList = _IDeliveryTerms_Repository.DuplicateEditDeliveryTerms(deliveryObj.TermsId, deliveryObj.DeliveryName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IDeliveryTerms_Repository.UpdateDeliveryTerms(deliveryObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Delivery Terms Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _IDeliveryTerms_Repository.DuplicateDeliveryTerms(deliveryObj.DeliveryName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IDeliveryTerms_Repository.AddDeliveryTerms(deliveryObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Delivery Terms Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update DeliveryTerms");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDeliveryTerms(int DeliveryTermsId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IDeliveryTerms_Repository.DeleteDeliveryTerms(Convert.ToInt32(DeliveryTermsId));
                DeliveryTermsMaster deliveryObj = new DeliveryTermsMaster();
                deliveryObj = _IDeliveryTerms_Repository.GetDeliveryTermsByID(DeliveryTermsId);
                deliveryObj.IsActive = false;
                _IDeliveryTerms_Repository.UpdateDeliveryTerms(deliveryObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete DeliveryTerms");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdDeliveryTerms(int DeliveryTermsId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                var obj = _IDeliveryTerms_Repository.GetDeliveryTermsByID(Convert.ToInt32(DeliveryTermsId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get DeliveryTerms by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IDeliveryTerms_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}