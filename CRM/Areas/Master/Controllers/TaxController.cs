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
    public class TaxController : Controller
    {
        private ITax_Repository _ITax_Repository;
        public TaxController()
        {
            this._ITax_Repository = new Tax_Repository(new elaunch_crmEntities());

        }
        // GET: Master/Tax
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTax()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTax(TaxMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_ITax_Repository.CheckTax(obj, false))
                    {
                        TaxMaster taxObj = new TaxMaster();
                        taxObj.TaxName = obj.TaxName;
                        taxObj.Percentage = obj.Percentage;
                        taxObj.IsActive = true;
                        _ITax_Repository.AddTax(taxObj); 
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Tax" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Invalid Percentage...!", null); 
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create Tax");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteTax(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TaxMaster taxObj = _ITax_Repository.GetTaxById(Convert.ToInt32(id));
                if (taxObj != null)
                {

                    taxObj.IsActive = false;
                    _ITax_Repository.UpdateTax(taxObj);
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
                ex.SetLog("Delete Tax");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateTax(TaxMaster taxObj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_ITax_Repository.CheckTax(taxObj, true))
                    {
                        taxObj.IsActive = true;
                        _ITax_Repository.UpdateTax(taxObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Tax" + MessageValue.Exists, null);
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
                ex.SetLog("Update Tax");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _ITax_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}