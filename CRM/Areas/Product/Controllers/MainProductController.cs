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

namespace CRM.Areas.Product.Controllers
{
    [HasLoginSessionFilter]
    public class MainProductController : Controller
    {
        private IMainProduct_Repository _IMainProduct_Repository;
        private IProduct_Repository _IProduct_Repository;

        public MainProductController()
        {
            this._IMainProduct_Repository = new MainProduct_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProduct_Repository = new Product_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMainProduct()
        {
            return View();
        }

        public JsonResult CreateMainProduct(MainProductMaster product)
        {
            DataResponse dataResponse = new DataResponse();
            var Isrepeat = _IMainProduct_Repository.IsProductExist(product,"ADD");
            if (!Isrepeat)
            {
                product.CreatedBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                product.CreatedDate = DateTime.Now.ToUniversalTime();
                product.IsActive = true;
                _IMainProduct_Repository.SaveMainProduct(product);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Product inserted successfully", null);
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Product alerady exist", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateMainProduct(MainProductMaster product)
        {
            DataResponse response = new DataResponse();
            var Isrepeat = _IMainProduct_Repository.IsProductExist(product,"EDIT");
            if (!Isrepeat)
            {
                //edit
                MainProductMaster objMainProduct = _IMainProduct_Repository.GetProductById(product.MainProductId);
                objMainProduct.MainProductName = product.MainProductName;
                objMainProduct.ModifyBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                objMainProduct.ModifyDate = DateTime.Now.ToUniversalTime();
                _IMainProduct_Repository.UpdateMainProduct(objMainProduct);
                response = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
            }
            else
            {
                response = CRMUtilities.GenerateApiResponse(MessageType.Error, "Product alerady exist", null);
            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetMainProductById(int id)
        {
            DataResponse response = new DataResponse();
            {
                try
                {
                    MainProductMaster objMainProduct = _IMainProduct_Repository.GetProductById(id);
                    response = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objMainProduct);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductBySubcategoryId(int id)
        {
            DataResponse response = new DataResponse();
            {
                try
                {
                    var objMainProduct = _IMainProduct_Repository.GetProductBySubcategoryId(id);
                    response = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objMainProduct);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMainProduct(int ProductId)
        {

            DataResponse dataResponse = new DataResponse();
            try
            {

                MainProductMaster prodObj = _IMainProduct_Repository.GetProductById(ProductId);
                if (prodObj != null)
                {
                    var prodct = _IProduct_Repository.GetProductBySubCategoryId(ProductId).ToList();
                    if (prodct.Count == 0)
                    {
                        prodObj.IsActive = false;
                        prodObj.DeletedBy = sessionUtils.UserId;
                        prodObj.DeletedDate = DateTime.Now.ToUniversalTime();
                        _IMainProduct_Repository.UpdateMainProduct(prodObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "This MainProduct use in Product so,Delete Product then Delete MainProduct.", null);
                    }

                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Error, null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }
    }
}