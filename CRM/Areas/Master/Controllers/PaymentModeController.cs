using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class PaymentModeController : Controller
    {
        private IPaymentMode_Repository _IPaymentMode_Repository;
        public PaymentModeController()
        {
            this._IPaymentMode_Repository = new PaymentMode_Repository(new elaunch_crmEntities());
        }
        // GET: Master/PaymentMode
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPaymentMode()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SavePaymentMode(PaymentModeMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                PaymentModeMaster paymentobj = new PaymentModeMaster();
                paymentobj.PaymentModeId = obj.PaymentModeId;
                paymentobj.PaymentMode = obj.PaymentMode;
                paymentobj.IsActive = true;
                if(paymentobj.PaymentModeId>0)
                {
                    var Cntlist = _IPaymentMode_Repository.DuplicateEditPaymentMode(paymentobj.PaymentModeId, paymentobj.PaymentMode).ToList();
                    if(Cntlist.Count==0)
                    {
                        _IPaymentMode_Repository.UpdatePaymentMode(paymentobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "PaymentMode Name Already Exists.", null);
                    }
                }
                else
                {
                    var Ctnlist = _IPaymentMode_Repository.DuplicatePaymentMode(paymentobj.PaymentMode).ToList();
                    if(Ctnlist.Count==0)
                    {
                        _IPaymentMode_Repository.AddPaymentMode(paymentobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "PaymentMode Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Payment Mode");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePaymentMode(int PaymentModeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                PaymentModeMaster paymentobj = new PaymentModeMaster();
                paymentobj= _IPaymentMode_Repository.GetPaymentModeByID(PaymentModeId);
                paymentobj.IsActive = false;
                _IPaymentMode_Repository.UpdatePaymentMode(paymentobj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Payment Mode");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByIdPaymentMode(int PaymentModeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IPaymentMode_Repository.GetPaymentModeByID(Convert.ToInt32(PaymentModeId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get PaymentMode By Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IPaymentMode_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}