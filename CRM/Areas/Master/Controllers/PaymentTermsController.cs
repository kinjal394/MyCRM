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
    public class PaymentTermsController : Controller
    {
        private IPaymentTerms_Repository _IPaymentTerms_Repository;

        public PaymentTermsController()
        {
            this._IPaymentTerms_Repository = new PaymentTerms_Repository(new elaunch_crmEntities());
        }


        // GET: Master/PaymentTerms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPaymentTerms()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SavePaymentTerms(PaymentTermsMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                PaymentTermsMaster paytermsObj = new PaymentTermsMaster();
                paytermsObj.PaymentTermId = d.PaymentTermId;
                paytermsObj.TermName = d.TermName.Trim();
                paytermsObj.Terms = d.Terms;
                paytermsObj.Description = d.Description.Trim();
                paytermsObj.IsActive = true;
                if (paytermsObj.PaymentTermId > 0)
                {
                    var cntList = _IPaymentTerms_Repository.DuplicateEditPaymentTerms(paytermsObj.PaymentTermId, paytermsObj.TermName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IPaymentTerms_Repository.UpdatePaymentTerms(paytermsObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Payment Terms Already Exists", null);
                    }
                }
                else
                {
                    var cntList = _IPaymentTerms_Repository.DuplicatePaymentTerms(paytermsObj.TermName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IPaymentTerms_Repository.AddPaymentTerms(paytermsObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Payment Terms Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update PaymentTerms");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePaymentTerms(int PaymentTermId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IPaymentTerms_Repository.DeletePaymentTerms(Convert.ToInt32(PaymentTermId));
                PaymentTermsMaster paytermsObj = new PaymentTermsMaster();
                paytermsObj = _IPaymentTerms_Repository.GetPaymentTermsByID(PaymentTermId);
                paytermsObj.IsActive = false;
                _IPaymentTerms_Repository.UpdatePaymentTerms(paytermsObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete PaymentTerms");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdPaymentTerms(int PaymentTermId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                var obj = _IPaymentTerms_Repository.GetPaymentTermsByID(Convert.ToInt32(PaymentTermId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get PaymentTerms");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IPaymentTerms_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}