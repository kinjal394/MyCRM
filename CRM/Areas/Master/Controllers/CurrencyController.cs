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
    public class CurrencyController : Controller
    {
        private ICurrency_Repository _ICurrency_Repository;

        public CurrencyController()
        {
            this._ICurrency_Repository = new Currency_Repository(new elaunch_crmEntities());

        }
        // GET: Master/EventType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCurrency()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveCurrency(CurrencyMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_ICurrency_Repository.CheckCurrencyType(obj, false))
                    {
                        CurrencyMaster currobj = new CurrencyMaster();
                        currobj.CurrencyName = obj.CurrencyName;
                        currobj.CurrencySymbol = obj.CurrencySymbol;
                        currobj.CurrencyCode = obj.CurrencyCode;
                        currobj.IsActive = true;
                        _ICurrency_Repository.AddCurrency(currobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Currency" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    //return PartialView("AddBank", obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                ex.SetLog("Create Currency");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteCurrency(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                CurrencyMaster Currobj = _ICurrency_Repository.GetCurrencyById(Convert.ToInt32(id));
                if (Currobj != null)
                {

                    Currobj.IsActive = false;

                    _ICurrency_Repository.UpdateCurrency(Currobj);
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
                ex.SetLog("Delete Currency");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SetCurrencyCode(string CurrencyName)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            var data = cm.GetAutoNumber("Currency", CurrencyName, 0);
            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
            {
                CurrencyData = data.Rows[0][0].ToString()
            });
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdateCurrency(CurrencyMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_ICurrency_Repository.CheckCurrencyType(obj, true))
                    {
                        obj.IsActive = true;
                        _ICurrency_Repository.UpdateCurrency(obj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Currency" + MessageValue.Exists, null);
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
                ex.SetLog("Update Currency");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _ICurrency_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}