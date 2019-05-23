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
    public class TransactionTypeController : Controller
    {
        private ITransactionType_Repository _ITransactionType_Repository;
        public TransactionTypeController()
        {
            this._ITransactionType_Repository = new TransactionType_Repository(new elaunch_crmEntities());
        }
        // GET: Master/TranscationType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddTransactionType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTransactionType(TransactionTypeMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                TransactionTypeMaster trantypeObj = new TransactionTypeMaster();
                trantypeObj.TranTypeId = d.TranTypeId;
                trantypeObj.TranType = d.TranType.Trim();
                trantypeObj.IsActive = true;
                if (trantypeObj.TranTypeId > 0)
                {
                    var cntList = _ITransactionType_Repository.DuplicateTransactionType(trantypeObj.TranTypeId, trantypeObj.TranType).ToList();
                    if (cntList.Count == 0)
                    {
                        _ITransactionType_Repository.UpdateTransactionType(trantypeObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Transactiontype Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _ITransactionType_Repository.DuplicateTransactionType(trantypeObj.TranType).ToList();
                    if (cntList.Count == 0)
                    {
                        _ITransactionType_Repository.AddTransactionType(trantypeObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Transactiontype Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update TransactionType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteTransactionType(int TranTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
               
                TransactionTypeMaster trantypeObj = new TransactionTypeMaster();
                trantypeObj = _ITransactionType_Repository.GetTransactionTypeByID(TranTypeId);
                trantypeObj.IsActive = false;
                _ITransactionType_Repository. UpdateTransactionType(trantypeObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TransactionType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdTransactionType(int TranTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _ITransactionType_Repository.GetTransactionTypeByID(Convert.ToInt32(TranTypeId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get TransactionType by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ITransactionType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}