using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using CRM_Repository.DTOModel;
using System.Configuration;
using CRM.App_Start;

namespace CRM.Areas.Product.Controllers
{
    [HasLoginSessionFilter]
    public class ProductController : Controller
    {
        private IProduct_Repository _IProduct_Repository;
        private IProductParameterMaster_Repository _IProductParameterMaster_Repository;
        private IProductPhotoMaster_Repository _IProductPhotoMaster_Repository;
        private ITechnicalSpecMaster_Repository _ITechnicalSpecMaster_Repository;
        private IProductCatalogMaster_Repository _IProductCatalogMaster_Repository;
        private IProductLinkMaster_Repository _IProductLinkMaster_Repository;
        private IProductVideoMaster_Repository _IProductVideoMaster_Repository;
        private IVendorContactDetail_Repository _IVendorContactDetail_Repository;
        private ISupplierContactDetail_Repository _ISupplierContactDetail_Repository;
        private IBuyerContactDetail_Repository _IBuyerContactDetail_Repository;

        public ProductController()
        {
            this._IProduct_Repository = new Product_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductParameterMaster_Repository = new ProductParameterMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductPhotoMaster_Repository = new ProductPhotoMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ITechnicalSpecMaster_Repository = new TechnicalSpecMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductCatalogMaster_Repository = new ProductCatalogMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductLinkMaster_Repository = new ProductLinkMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProductVideoMaster_Repository = new ProductVideoMaster_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IVendorContactDetail_Repository = new VendorContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISupplierContactDetail_Repository = new SupplierContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IBuyerContactDetail_Repository = new BuyerContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Product/Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddApplicablecharges()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUpdateProduct(ProductMaster objProduct)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    bool isExist = _IProduct_Repository.IsExist(objProduct.ProductId, objProduct.ProductName, objProduct.SubCategoryId);
                    if (!isExist)
                    {
                        if (objProduct.ProductId <= 0)
                        {
                            objProduct.IsActive = true;
                            objProduct.CreatedBy = sessionUtils.UserId;
                            objProduct.CreatedDate = DateTime.Now;
                            objProduct.HSCode = (objProduct.HSCode != null && objProduct.HSCode != "") ? objProduct.HSCode : "0";
                            _IProduct_Repository.saveProduct(objProduct);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            ProductMaster objProductMaster = _IProduct_Repository.GetProductById(objProduct.ProductId);
                            objProductMaster.ProductId = objProduct.ProductId;
                            objProductMaster.SubCategoryId = objProduct.SubCategoryId;
                            objProductMaster.ProductName = objProduct.ProductName;
                            objProductMaster.HSCode = (objProduct.HSCode != null && objProduct.HSCode != "") ? objProduct.HSCode : "0";
                            objProductMaster.Keywords = objProduct.Keywords;
                            objProductMaster.ProductCode = objProduct.ProductCode;
                            objProductMaster.Functionality = objProduct.Functionality;
                            objProductMaster.ModifyBy = sessionUtils.UserId;
                            //objProductMaster.OursModelNo = objProduct.OursModelNo;
                            objProductMaster.ModifyDate = DateTime.Now;
                            _IProduct_Repository.UpdateProduct(objProductMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Product Already Exists", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Product");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteProduct(int ProductId)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    _IProduct_Repository.DeleteProduct(ProductId, UserId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Deleted Succesfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Product");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductById(int ProductId)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    ProductMaster objProductMaster = _IProduct_Repository.GetProductById(ProductId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objProductMaster);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Product by Id");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);

                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public JsonResult GetProductByCatId(int id)
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    if (sessionUtils.HasUserLogin())
        //    {
        //        try
        //        {
        //            var objProductpacking = _IProduct_Repository.FetchByCatId(id);
        //            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", objProductpacking);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
        //        }
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public JsonResult GetAllProductFormInfoById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    ProductFormModel objProductFormModel = _IProduct_Repository.FetchById(Id);
                    var data = cm.GetAutoNumber("Product", objProductFormModel.ProductName, objProductFormModel.ProductId);
                    objProductFormModel.ProductCode = data.Rows[0][0].ToString();
                    List<ProductPhotoMaster> objProductPhotoMaster = _IProductPhotoMaster_Repository.GetPhotoByProductId(Id).ToList();
                    List<vmProductParameterMaster> objProductParameterMaster = _IProductParameterMaster_Repository.GetTechDetailByProductId(Id).ToList();
                    List<ProductCatalogMaster> objProductCatalogMaster = _IProductCatalogMaster_Repository.GetCatalogByProductId(Id).ToList();
                    List<ProductLinkMaster> objProductSocialMaster = _IProductLinkMaster_Repository.GetLinkByProductId(Id).ToList();
                    List<ProductVideoMaster> objProductVideoMaster = _IProductVideoMaster_Repository.GetVideoByProductId(Id).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objProductFormModel = objProductFormModel,
                        objProductPhotoMaster = objProductPhotoMaster,
                        objProductParameterMaster = objProductParameterMaster,
                        objProductCatalogMaster = objProductCatalogMaster,
                        objProductSocialMaster = objProductSocialMaster,
                        objProductVideoMaster = objProductVideoMaster,
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All Product Info");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);

                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetProductCode(string ProductName)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            var data = cm.GetAutoNumber("Product", ProductName, 0);
            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
            {
                PrdData = data.Rows[0][0].ToString()
            });
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetAllProductSupplierInfoById(int Id, int catelogId)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<ProductCatalogMaster> objProductCatalogMaster = _IProductCatalogMaster_Repository.GetCatalogByProductSupplierId(Id, catelogId).ToList();
                    List<ProductPhotoMaster> objProductPhotoMaster = _IProductPhotoMaster_Repository.GetPhotoByProductSupplierId(Id, catelogId).ToList();
                    List<vmProductParameterMaster> objProductParameterMaster = _IProductParameterMaster_Repository.GetTechDetailByProductSupplierId(Id, catelogId).ToList();
                    List<ProductLinkMaster> objProductSocialMaster = _IProductLinkMaster_Repository.GetLinkByProductSupplierId(Id, catelogId).ToList();
                    List<ProductVideoMaster> objProductVideoMaster = _IProductVideoMaster_Repository.GetVideoByProductSupplierId(Id, catelogId).ToList();
                    List<ProductPackingDetail> objProductPackingMaster = _IProductCatalogMaster_Repository.GetPackingByProductSupplierId(Id, catelogId).ToList();
                    List<ProductSuppDocumentDetail> objProductSuppDocumentDetail = _IProductCatalogMaster_Repository.GetSuppDocumentByProductSuppId(Id, catelogId).ToList();
                    List<ProductPrice> objproductprice = _IProduct_Repository.GetProductPriceById(Id, catelogId).ToList();
                    List<ProductApplicableCharge> objProductApplicable = _IProduct_Repository.GetProductApplicableChargeById(Id).ToList();
                    ProductMaster obj = _IProduct_Repository.GetProductById(Id);

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objProductCatalogMaster = objProductCatalogMaster,
                        objProductPhotoMaster = objProductPhotoMaster,
                        objProductParameterMaster = objProductParameterMaster,
                        objProductSocialMaster = objProductSocialMaster,
                        objProductVideoMaster = objProductVideoMaster,
                        objProductPackingMaster = objProductPackingMaster,
                        objProductSuppDocumentDetail = objProductSuppDocumentDetail,
                        productdata = obj,
                        objProductpriceMaster = objproductprice,
                        objProductApplicableCharge = objProductApplicable
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All Product Info");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);

                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTechnicalSpecMaster()
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    List<TechnicalSpecMaster> ListTechnicalSpecMaster = _ITechnicalSpecMaster_Repository.GetTechnicalSpec().ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", ListTechnicalSpecMaster);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get TechnicalSpec");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProductFrom(ProductFormModel objProductFormModel)
        {
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string productImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ProductImagePath"]);
            string productCatalogue = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ProductCataloguePath"]);
            string productDocument = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ProductImgPath"]);
            string SupplierCatalogimg = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["SupplierCatalogimg"]);
            string OurCatalogImage = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["OurCatalogImage"]);

            if (!System.IO.Directory.Exists(productImgPath))
            {
                System.IO.Directory.CreateDirectory(productImgPath);
            }
            if (!System.IO.Directory.Exists(productCatalogue))
            {
                System.IO.Directory.CreateDirectory(productCatalogue);
            }
            if (!System.IO.Directory.Exists(productDocument))
            {
                System.IO.Directory.CreateDirectory(productDocument);
            }
            if (!System.IO.Directory.Exists(SupplierCatalogimg))
            {
                System.IO.Directory.CreateDirectory(SupplierCatalogimg);
            }
            if (!System.IO.Directory.Exists(OurCatalogImage))
            {
                System.IO.Directory.CreateDirectory(OurCatalogImage);
            }
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    objProductFormModel.CreatedBy = sessionUtils.UserId;
                    objProductFormModel.ModifyBy = sessionUtils.UserId;
                    objProductFormModel.DeletedBy = sessionUtils.UserId;
                    int ResponseVal = _IProduct_Repository.UpdateProductFrom(objProductFormModel);

                    if (ResponseVal == 1)
                    {
                        if (objProductFormModel.ProductPhotoMasters != null)
                        {
                            if (objProductFormModel.ProductPhotoMasters.Count > 0)
                            {
                                foreach (var item in objProductFormModel.ProductPhotoMasters)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.Photo.ToString()))
                                    {
                                        if (!System.IO.File.Exists(productImgPath.ToString() + "/" + item.Photo.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.Photo.ToString(), productImgPath.ToString() + "/" + item.Photo.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (objProductFormModel.ProductSuppDocumentDetail != null)
                        {
                            if (objProductFormModel.ProductSuppDocumentDetail.Count > 0)
                            {
                                foreach (var item in objProductFormModel.ProductSuppDocumentDetail)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.DocPath.ToString()))
                                    {
                                        if (!System.IO.File.Exists(productDocument.ToString() + "/" + item.DocPath.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.DocPath.ToString(), productDocument.ToString() + "/" + item.DocPath.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (objProductFormModel.OurCatalogImg != null)
                        {
                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objProductFormModel.OurCatalogImg.ToString()))
                            {
                                if (!System.IO.File.Exists(OurCatalogImage.ToString() + "/" + objProductFormModel.OurCatalogImg.ToString()))
                                {
                                    System.IO.File.Move(TempImgPath.ToString() + "/" + objProductFormModel.OurCatalogImg.ToString(), OurCatalogImage.ToString() + "/" + objProductFormModel.OurCatalogImg.ToString());
                                }
                            }
                        }
                        if (objProductFormModel.SupplierCatalogimg != null)
                        {
                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objProductFormModel.SupplierCatalogimg.ToString()))
                            {
                                if (!System.IO.File.Exists(SupplierCatalogimg.ToString() + "/" + objProductFormModel.SupplierCatalogimg.ToString()))
                                {
                                    System.IO.File.Move(TempImgPath.ToString() + "/" + objProductFormModel.SupplierCatalogimg.ToString(), SupplierCatalogimg.ToString() + "/" + objProductFormModel.SupplierCatalogimg.ToString());
                                }
                            }
                        }
                        //IMAGES TRANSFER FROM UPLOAD FILE TO 
                        //dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully", null);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Successfully", new
                        {
                            objProductPhotoMaster = _IProductPhotoMaster_Repository.GetPhotoByProductId(objProductFormModel.ProductId).ToList(),
                            objProductCatalogMaster = _IProductCatalogMaster_Repository.GetCatalogByProductId(objProductFormModel.ProductId).ToList(),
                            ProductSuppDocumentDetail = _IProductCatalogMaster_Repository.GetSuppDocumentbyId(objProductFormModel.ProductId).ToList(),
                        });
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Update Product");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
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
                string mail = "";
                List<dynamic> data = null;
                string hst = HttpContext.Request.Url.PathAndQuery;
                String source = HttpContext.Request.Url.AbsoluteUri.Replace(hst, "/");
                if (objProductFormModel.ProductCatalogMasters != null && objProductFormModel.mode == "Catalogue")
                {
                    data = objProductFormModel.ProductCatalogMasters.Where(x => x.share == true).ToList<dynamic>();
                }
                else if (objProductFormModel.ProductPhotoMasters != null && objProductFormModel.mode == "Photo")
                {
                    data = objProductFormModel.ProductPhotoMasters.Where(x => x.share == true).ToList<dynamic>();
                }
                else if (objProductFormModel.ProductVideoMasters != null && objProductFormModel.mode == "Video")
                {
                    data = objProductFormModel.ProductVideoMasters.Where(x => x.share == true).ToList<dynamic>();
                }
                else if (objProductFormModel.ProductSocialMasters != null && objProductFormModel.mode == "Social")
                {
                    data = objProductFormModel.ProductSocialMasters.Where(x => x.share == true).ToList<dynamic>();
                }
                string contactperson = data[0].ContactPersonType;
                int contactpersonid = int.Parse(data[0].ContactPerson.ToString());
                if (contactperson == "SupplierMaster")
                {
                    var dat = _ISupplierContactDetail_Repository.GetBySupplierId(contactpersonid).Select(x => x.Email).ToString();
                    foreach (var item in dat)
                    {
                        if (mail != "")
                            mail += "," + item.ToString();
                        else
                            mail += item.ToString();
                        //mail = "elaunch.rajeshvari@gmail.com";

                    }
                }
                else if (contactperson == "VendorMaster")
                {
                    var dat = _IVendorContactDetail_Repository.GetByVendorId(contactpersonid).Select(x => x.Email).ToList();
                    foreach (var item in dat)
                    {
                        if (mail != "")
                            mail += "," + item.ToString();
                        else
                            mail += item.ToString();
                        //mail = "elaunch.rajeshvari@gmail.com";

                    }
                }
                else if (contactperson == "BuyerMaster")
                {
                    var dat = _IBuyerContactDetail_Repository.GetById(contactpersonid).Select(x => x.Email).ToList();
                    foreach (var item in dat)
                    {
                        //if (mail != "")
                        //    mail += "," + item.ToString();
                        //else
                        //    mail += item.ToString();
                        mail = "elaunch.rajeshvari@gmail.com";

                    }
                }
                foreach (var item in data)
                {
                    //bodydata += "<br /><a href=\"" + source.ToString() + "\\" + item.CatalogPath + "\">here</ID></a>";
                    if (objProductFormModel.mode == "Catalogue")
                    {
                        var linkname = Path.GetFileName(item.CatalogPath);
                        var link = source.ToString() + item.CatalogPath;
                        bodydata += "<br />Click here.<a href=\"" + link + "\">" + linkname + "</a>";
                    }
                    else if (objProductFormModel.mode == "Photo")
                    {
                        var linkname = Path.GetFileName(item.Photo);
                        var link = source.ToString() + item.Photo;
                        bodydata += "<br />Click here.<a href=\"" + link + "\">" + linkname + "</a>";
                    }
                    else if (objProductFormModel.mode == "Video")
                    {
                        var link = item.URL;
                        bodydata += "<br />Click here.<a href=\"" + link + "\">" + link + "</a>";
                    }
                    else if (objProductFormModel.mode == "Social")
                    {
                        var link = item.Url;
                        bodydata += "<br />Click here.<a href=\"" + link + "\">" + link + "</a>";
                    }

                }
                string body = "<p> Hello,</p>";
                body += "<p> Your Atteched File Is : " + bodydata + "</p>";
                body += " <p>Regards,<br /> CRM</p>";
                //cm.sendmail(mail, body, "Mail Send");
                //dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Email Send", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Send File");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckBuyerPrdModelNo(string ModelNo)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    if (_IProductCatalogMaster_Repository.CheckProductModenolDuplication(ModelNo))
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Our Product Model no." + MessageValue.Exists, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All Product Info");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);

                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckSupplierModelNo(string SupmodelNo)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    if (_IProductCatalogMaster_Repository.CheckSupplierModelNolDuplication(SupmodelNo))
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Supplier Side Model No." + MessageValue.Exists, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get All Product Info");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);

                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult PrintKeyword(string Key)
        {
            Response.Clear();
            Response.AddHeader("Content-disposition", string.Format("attachment; filename=\"{0}\";", "keyword.txt"));
            return File(System.Text.Encoding.ASCII.GetBytes(Key), "text/plain");
        }
        
    }
}



//Unnecessary
//[HttpPost]
//public ActionResult SaveProduct(ProductMaster data)
//{
//    DataResponse dataResponse = new DataResponse();
//    try
//    {
//        bool isExist = _IProduct_Repository.IsExist(data.ProductId, data.ProductName);
//        if (!isExist)
//        {

//            string path = HostingEnvironment.ApplicationPhysicalPath;
//            var SourcePath = path + "UploadImages/TempImg/";
//            data.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//            data.IsActive = true;
//            data.CreatedBy = Convert.ToInt32(Session["UserId"]);
//            _IProduct_Repository.saveProduct(data);
//            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Added Successfully", null);
//        }
//        else
//        {
//            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);
//        }
//    }
//    catch (Exception ex)
//    {
//        return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
//    }
//    return Json(dataResponse, JsonRequestBehavior.AllowGet);
//}

//[HttpPost]
//public JsonResult UpdateProduct(ProductMaster Data)
//{
//    DataResponse dataResponse = new DataResponse();
//    try
//    {
//        bool isExist = _IProduct_Repository.IsExist(Data.ProductId, Data.ProductName);
//        if (!isExist)
//        {
//            string path = HostingEnvironment.ApplicationPhysicalPath;
//            var SourcePath = path + "\\TempFolder";
//            int UserId = Convert.ToInt32(Session["UserId"]);
//            var data = _IProduct_Repository.UpdateProductDetail(Data, UserId);
//            if (data == 1)
//                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
//            else
//                return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
//        }
//        else
//        {
//            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);
//        }
//    }
//    catch (Exception ex)
//    {
//        return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
//    }
//    return Json(dataResponse, JsonRequestBehavior.AllowGet);
//}

//public JsonResult GetProductData(int id)
//{
//    DataResponse dataResponse = new DataResponse();
//    try
//    {
//        var data = _IProduct_Repository.ProductData(id);
//        string JS = DataTableToJSONWithJSONNet(data);
//        return Json(JS, JsonRequestBehavior.AllowGet);
//    }
//    catch (Exception ex)
//    {
//        return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
//    }
//}

//public string DataTableToJSONWithJSONNet(DataTable table)
//{
//    string JSONString = string.Empty;
//    JSONString = JsonConvert.SerializeObject(table);
//    return JSONString;
//}