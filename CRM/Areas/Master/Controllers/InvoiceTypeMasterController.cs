using CRM.App_Start;
using CRM.Models;
using CRM.Models.Grid;
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
    public class InvoiceTypeMasterController : Controller
    {
        // GET: Master/InvoiceTypeMaster

        private InvoiceType_Repository _IInvoiceType_Repository;
        public InvoiceTypeMasterController()
        {
            this._IInvoiceType_Repository = new InvoiceType_Repository(new elaunch_crmEntities());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddInvoiceType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveInvoiceType(CRM_Repository.Data.InvoiceTypeMaster IQR)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (Session["UserId"] != null)
                {
                    var Dub = _IInvoiceType_Repository.DuplicateTInvoiceType(IQR).ToList();
                    if (Dub.Count <= 0)
                    {
                        IQR.IsActive = true;
                        _IInvoiceType_Repository.AddInvoiceType(IQR);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Added successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Dublicate Invoice Type not allowed", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }

            }
            catch (Exception ex)
            {
                ex.SetLog("Create InvoiceType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }



        public JsonResult UpdateInvoiceType(CRM_Repository.Data.InvoiceTypeMaster Data)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var Dub = _IInvoiceType_Repository.DuplicateTInvoiceType(Data).ToList();
                if (Dub.Count <= 0)
                {
                    Data.IsActive = true;
                    _IInvoiceType_Repository.UpdateInvoiceType(Data);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Updated successfully", null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Dublicate Invoice Type not allowed", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Update InvoiceType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteInvoiceType(int InvoiceTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                _IInvoiceType_Repository.DeleteInvoiceType(InvoiceTypeId);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Deleted successfully", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete InvoiceType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IInvoiceType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}