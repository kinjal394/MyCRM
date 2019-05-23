using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.App_Start;
using CRM.Models;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class SalesDocumentController : Controller
    {
        private ISalesDocumentName_Repository _ISalesDocumentName_Repository;
        public SalesDocumentController()
        {
            this._ISalesDocumentName_Repository = new SalesDocumentName_Repository(new elaunch_crmEntities());
        }
        // GET: Master/SalesDocument
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSalesDocument()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveSalesDocument(SalesDocumentNameMaster Obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                SalesDocumentNameMaster SalesObj = new SalesDocumentNameMaster();
                SalesObj.SalesDocId = Obj.SalesDocId;
                SalesObj.SalesDocument = Obj.SalesDocument;
                SalesObj.IsActive = true;
                if(SalesObj.SalesDocId>0)
                {
                    var Cntlist = _ISalesDocumentName_Repository.DuplicateEditSalesDocument(SalesObj.SalesDocId, SalesObj.SalesDocument).ToList();
                    if(Cntlist.Count==0)
                    {
                        _ISalesDocumentName_Repository.UpdateSalesDocument(SalesObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "SalesDocument Name Already Exists.", null);
                    }
                }
                else
                {
                    var Cntlist = _ISalesDocumentName_Repository.DuplicateSalesDocument(SalesObj.SalesDocument).ToList();
                    if(Cntlist.Count==0)
                    {
                        _ISalesDocumentName_Repository.AddSalesDocument(SalesObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "SalesDocument Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update SalesDocument Name");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);       
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSalesDocument(int SalesDocId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                SalesDocumentNameMaster salesObj = new SalesDocumentNameMaster();
                salesObj = _ISalesDocumentName_Repository.GetSalesDocumentById(SalesDocId);
                salesObj.IsActive = false;
                _ISalesDocumentName_Repository.UpdateSalesDocument(salesObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete SalesDocument Name");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByIdSalesDocument(int SalesDocId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _ISalesDocumentName_Repository.GetSalesDocumentById(Convert.ToInt32(SalesDocId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get SalesDocument By Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ISalesDocumentName_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}