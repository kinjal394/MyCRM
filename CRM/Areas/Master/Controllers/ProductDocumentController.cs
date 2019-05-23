using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.App_Start;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;

namespace CRM.Areas.Master.Controllers
{
    public class ProductDocumentController : Controller
    {
        private IProductDocument_Repository _IProductDocument_Repository;

        public ProductDocumentController()
        {
            this._IProductDocument_Repository = new ProductDocument_Repository(new elaunch_crmEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProductDocument()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveProductDocument(ProductDocumentMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ProductDocumentMaster ProductDocumentObj = new ProductDocumentMaster();
                ProductDocumentObj.PrdDocId = d.PrdDocId;
                ProductDocumentObj.PrdDocName = d.PrdDocName.Trim();
                ProductDocumentObj.IsActive = true;
                if (ProductDocumentObj.PrdDocId > 0)
                {
                    var cntList = _IProductDocument_Repository.DuplicateEditProductDocument(ProductDocumentObj.PrdDocId, ProductDocumentObj.PrdDocName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IProductDocument_Repository.UpdateProductDocument(ProductDocumentObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Product Document Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IProductDocument_Repository.DuplicatProductDocument(ProductDocumentObj.PrdDocName).ToList();
                    if (cntList.Count == 0)
                    {
                        _IProductDocument_Repository.AddProductDocument(ProductDocumentObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Product Document Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Product Document");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteProductDocument(int PrdDocId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                ProductDocumentMaster ProductDocumentObj = new ProductDocumentMaster();
                ProductDocumentObj = _IProductDocument_Repository.GetProductDocumentByID(PrdDocId);
                ProductDocumentObj.IsActive = false;
                _IProductDocument_Repository.UpdateProductDocument(ProductDocumentObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Agency Type");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByIdProductDocument(int PrdDocId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IProductDocument_Repository.GetProductDocumentByID(Convert.ToInt32(PrdDocId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get AgencyType by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IProductDocument_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}