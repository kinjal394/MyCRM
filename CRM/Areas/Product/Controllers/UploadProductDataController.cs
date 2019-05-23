using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM_Repository.DTOModel;
using CRM.App_Start;
using System.IO;

namespace CRM.Areas.Product.Controllers
{
    [HasLoginSessionFilter]
    public class UploadProductDataController : Controller
    {
        private IUploadProductData_Repository _IUploadProductData_Repository;
        private IProduct_Repository _IProduct_Repository;
        private IProductLinkMaster_Repository _IProductLinkMaster_Repository;
        private IProductVideoMaster_Repository _IProductVideoMaster_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;
        public UploadProductDataController()
        {
            this._IUploadProductData_Repository = new UploadProductData_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProduct_Repository = new Product_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductLinkMaster_Repository = new ProductLinkMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductVideoMaster_Repository = new ProductVideoMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Product/UploadProductData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageUploadProductData(int id = 0, int temp = 0, int cid = 0)
        {
            ViewBag.id = id;
            ViewBag.cid = cid;
            ViewBag.isdisable = temp;
            return View();
        }

        [HttpGet]
        public JsonResult GetProductDetailById(int ProductId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ProductMaster objProductData = _IProduct_Repository.GetProductById(ProductId);
                List<UploadProductDataModel> objUploadProductData = _IUploadProductData_Repository.FetchById(ProductId);

                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                {
                    objProductData = objProductData,
                    objUploadProductData = objUploadProductData
                });
            }
            catch (Exception ex)
            {
                ex.SetLog("Get UploadProductData by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductDetailByCId(int ProductId, int CatalogId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ProductMaster objProductData = _IProduct_Repository.GetProductById(ProductId);
                List<UploadProductDataModel> objUploadProductData = _IUploadProductData_Repository.FetchById(ProductId);
                List<ProductLinkMaster> objProductSocialMaster = _IProductLinkMaster_Repository.GetLinkByProductSupplierId(ProductId, CatalogId).ToList();
                List<ProductVideoMaster> objProductVideoMaster = _IProductVideoMaster_Repository.GetVideoByProductSupplierId(ProductId, CatalogId).ToList();
                //List<UploadProductDataModel> objUploadProductData = _IUploadProductData_Repository.FetchById(ProductId, CatalogId);

                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                {
                    objProductData = objProductData,
                    objUploadProductData = objUploadProductData,
                    objProductVideoMaster = objProductVideoMaster,
                    objProductSocialMaster = objProductSocialMaster
                });
            }
            catch (Exception ex)
            {
                ex.SetLog("Get UploadProductData by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdate(UploadProductDataModel objInputAdvLinkMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputAdvLinkMaster.CreatedBy = sessionUtils.UserId;
                    objInputAdvLinkMaster.ModifyBy = sessionUtils.UserId;
                    objInputAdvLinkMaster.DeletedBy = sessionUtils.UserId;
                    int ResponseVal = _IUploadProductData_Repository.CreateUpdate(objInputAdvLinkMaster);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                    }
                    else if (ResponseVal == 2)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update UploadProductData");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    UploadProductDataModel objInputAdvLinkMaster = new UploadProductDataModel();
                    objInputAdvLinkMaster.AdId = id;
                    objInputAdvLinkMaster.DeletedBy = sessionUtils.UserId;

                    int ResponseVal = _IUploadProductData_Repository.Delete(objInputAdvLinkMaster);
                    //ResponseVal 1: Delete,0: error
                    if (ResponseVal == 1)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete UploadProductData");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllUploadProductDataInfoById(string code)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<UploadProductDataModel> objUploadProductData = _IUploadProductData_Repository.FetchByCode(code);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objUploadProductData = objUploadProductData
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get AllUploadProductData by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    throw;
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendFile(ProductFormModel objProductFormModel)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                string bodydata = "";
                string hst = HttpContext.Request.Url.PathAndQuery;
                String source = HttpContext.Request.Url.AbsoluteUri.Replace(hst, "/");

                if (objProductFormModel.ProductVideoMasters != null)
                {
                    bodydata += "<b>Video Link Detail</b>";
                    foreach (var item in objProductFormModel.ProductVideoMasters)
                    {
                        var link = item.URL;
                        bodydata += "<br /><a href=\"" + link + "\">" + link + "</a>";
                    }
                }
                if (objProductFormModel.ProductSocialMasters != null)
                {
                    bodydata += "<br/><br/><b>Source Link Detail</b>";
                    foreach (var item in objProductFormModel.ProductSocialMasters)
                    {
                        var link = item.Url;
                        bodydata += "<br /><a href=\"" + link + "\">" + link + "</a>";
                    }
                }
                string body = "<p> Hello Dear,</p>";
                body += "<p> Product Details : <br />" + bodydata + "</p>";
                body += " <p>Regards,<br /> <b>Gurjari Services Ltd.</b></p>";
                if (objProductFormModel.EmailId != null)
                {
                    string[] Emailarray = objProductFormModel.EmailId.Split(',');
                    foreach (string Email in Emailarray)
                    {
                        string mail = Email.ToString();
                        cm.sendmail(mail.ToString(), body, "Gurjari Product Sharing", "sales@gurjarimall.com", "aXWbBRrL3LPXfYBNKt8nrA==");
                    }
                }
                //EmailSpeechMaster SpeechEmail = _IEmailSpeech_Repository.CheckEmailSpeech("Quotation Speech"); // To GET EMAIL Speech used Email Tile.
                // GET Buyer Email ID.
                //var dat = _IBuyerContactDetail_Repository.GetById(Convert.ToInt32(objProductFormModel.mode)).Select(x => x.Email).ToList();
                // foreach (var item in dat)
                //{
               // if (!string.IsNullOrEmpty(objProductFormModel.EmailId))
                  //  cm.sendmail(objProductFormModel.EmailId.ToString(), body, "Gurjari Product Sharing", "sales@gurjarimall.com", "aXWbBRrL3LPXfYBNKt8nrA==");
                //}
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Email Send", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Send File");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}