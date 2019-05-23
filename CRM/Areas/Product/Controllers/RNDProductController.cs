using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.ExtendedModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Product.Controllers
{
    [HasLoginSessionFilter]
    public class RNDProductController : Controller
    {
        private IRndProduct_Repository _IRndProduct_Repository;
        private IRndSupplier_Repository _IRndSupplier_Repository;
        private IEmailSpeech_Repository _IEmailSpeech_Repository;
        private ISMSSpeech_Repository _ISMSSpeech_Repository;
        public RNDProductController()
        {
            this._IRndProduct_Repository = new RndProduct_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IRndSupplier_Repository = new RndSupplier_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IEmailSpeech_Repository = new EmailSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISMSSpeech_Repository = new SMSSpeech_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Product/RNDProduct
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult AddProduct(int? id = 0)
        //{
        //    int setid = Convert.ToInt32(id);
        //    if (id == 0) return View();
        //    RndProductModel model = new RndProductModel();
        //    model.Product = _IRndProduct_Repository.GetProductById(setid);
        //    model.SupplierList = _IRndSupplier_Repository.GetRndSupplierByProductId(setid);
        //    return View(model);
        //}
        public ActionResult AddProduct(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }
        public ActionResult AddSupplier()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetAllRndProductInfoById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    RNDProductModel objRNDProductMaster = _IRndProduct_Repository.GetAllRndProductInfoById(Id);
                    List<RNDSupplierMaster> objRNDSupplierMaster = _IRndProduct_Repository.GetAllRNDSupplierDetail(Id).ToList();
                    var data = new
                    {
                        objRndProductMaster = objRNDProductMaster,
                        objRndSupplierDetail = objRNDSupplierMaster
                    };
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get R & D Product");
                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUpdate(RNDProductModel objInputRndProduct)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    objInputRndProduct.CreatedBy = sessionUtils.UserId;
                    objInputRndProduct.ModifyBy = sessionUtils.UserId;
                    objInputRndProduct.DeleteBy = sessionUtils.UserId;
                    int ResponseVal = _IRndProduct_Repository.CreateUpdate(objInputRndProduct);
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
                ex.SetLog("Create R & D Product");
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
                    RNDProductModel objInputRndProduct = new RNDProductModel();
                    objInputRndProduct.RNDProductId = id;
                    objInputRndProduct.DeleteBy = sessionUtils.UserId;

                    int ResponseVal = _IRndProduct_Repository.Delete(objInputRndProduct);
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
                ex.SetLog("Delete R & D Product");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCompanyDetailById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    SupplierDetailByIdModel objRNDSupplierMaster = _IRndSupplier_Repository.GetCompanyDetailById(Id);
                    var data = new
                    {
                       objRndSupplierDetail = objRNDSupplierMaster
                    };
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get R & D Product");
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
        public JsonResult GetEmailSpeechDetailById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    EmailSpeechMaster objEmailSpeechMaster = _IEmailSpeech_Repository.GetEmailSpeechById(Id);
                    var data = new
                    {
                        objobjEmailSpeechMaster = objEmailSpeechMaster
                    };
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get R & D Product");
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
        public JsonResult GetSMSSpeechDetailById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {

                    SMSSpeechMaster objSMSSpeechMaster = _ISMSSpeech_Repository.GetSMSSpeechById(Id);
                    var data = new
                    {
                        objSMSSpeechMaster = objSMSSpeechMaster
                    };
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get R & D Product");
                    return Json(CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null));
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult SaveProduct(RndProductModel modal)
        //{
        //    bool status = false;
        //    if (ModelState.IsValid)
        //    {
        //        string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
        //        bool exists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["RndImgPath"]));

        //        if (!exists)
        //            System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["RndImgPath"]));
        //        string RndImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["RndImgPath"]);



        //        //if (System.IO.File.Exists(TempImgPath.ToString() + "/" + modal.Product.Cataloges.ToString()))
        //        //               {
        //        //                   System.IO.File.Move(TempImgPath.ToString() + "/" + objPlugShape.Photo.ToString(), PlugShapeImgPath.ToString() + "/" + objPlugShape.Photo.ToString());
        //        //               }
        //        modal.Product.CreatedBy = sessionUtils.UserId;
        //        _IRndProduct_Repository.SaveProduct(modal.Product, modal.SupplierList);
        //        status = true;
        //    }
        //    else
        //    {
        //        status = false;
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}

    }

}