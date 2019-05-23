using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM_Repository.Data;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class DocumentController : Controller
    {
        private IDocument_Repository _IDocument_Repository;

        public DocumentController()
        {
            this._IDocument_Repository = new Document_Repository(new elaunch_crmEntities());

        }
        // GET: Master/Document
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddDoc()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveDoc(DocumentNameMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_IDocument_Repository.CheckDocName(obj, false))
                    {
                        DocumentNameMaster Docobj = new DocumentNameMaster();
                        Docobj.DocName = obj.DocName;
                        Docobj.IsActive = true;
                        _IDocument_Repository.AddDocument(Docobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Document" + MessageValue.Exists, null);
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
                ex.SetLog("Create Document");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteDoc(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                DocumentNameMaster docobj = _IDocument_Repository.GetDocById(Convert.ToInt32(id));
                if (docobj != null)
                {

                    docobj.IsActive = false;

                    _IDocument_Repository.UpdateDocument(docobj);
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
                ex.SetLog("Delete Document");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpPost]
        public JsonResult UpdateDoc(DocumentNameMaster Docobj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                // BankMaster obj = _IBank_Repository.GetBankById(bank.BankId);
                if (ModelState.IsValid)
                {
                    if (!_IDocument_Repository.CheckDocName(Docobj, true))
                    {
                        Docobj.IsActive = true;
                        _IDocument_Repository.UpdateDocument(Docobj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Document" + MessageValue.Exists, null);
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
                ex.SetLog("Update Document");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _IDocument_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
