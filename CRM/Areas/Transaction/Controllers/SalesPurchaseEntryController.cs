using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Transaction.Controllers
{
    public class SalesPurchaseEntryController : Controller
    {
        private CRM_Repository.Data.elaunch_crmEntities context = new CRM_Repository.Data.elaunch_crmEntities();
        private ISalesPurchaseEntry_Repository _ISalesPurchaseEntry_Repository;
        public SalesPurchaseEntryController()
        {
            this._ISalesPurchaseEntry_Repository = new SalesPurchaseEntry_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Transaction/SalesPurchaseEntry
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalesPurchaseEntryDetail(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }
        [HttpPost]
        public JsonResult CreateUpdate(SalesPurchaseEntryMaster objSalesPurchase)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                    string SalesPurchaseEntryPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["SalesPurchaseEntryPath"]);
                    if (!System.IO.Directory.Exists(SalesPurchaseEntryPath))
                    {
                        System.IO.Directory.CreateDirectory(SalesPurchaseEntryPath);
                    }
                    objSalesPurchase.CreatedBy= sessionUtils.UserId;
                    objSalesPurchase.CreatedDate = DateTime.Now;
                    objSalesPurchase.ModifyBy = sessionUtils.UserId;
                    objSalesPurchase.ModifyDate = DateTime.Now;
                    int ResponseVal = _ISalesPurchaseEntry_Repository.CreateUpdateSalePurchaseEntry(objSalesPurchase);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1)
                    {
                        if (objSalesPurchase.SalesPurchaseDocMasters != null)
                        {
                            if (objSalesPurchase.SalesPurchaseDocMasters.Count > 0)
                            {
                                foreach (var item in objSalesPurchase.SalesPurchaseDocMasters)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.DocPath.ToString()))
                                    {
                                        if (!System.IO.File.Exists(SalesPurchaseEntryPath.ToString() + "/" + item.DocPath.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.DocPath.ToString(), SalesPurchaseEntryPath.ToString() + "/" + item.DocPath.ToString());
                                        }
                                    }
                                }
                            }
                        }
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
                catch (Exception ex)
                {
                    ex.SetLog("CreateUpdate SalesPurchaseEntry");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteSalePurchaseEntry(int id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    SalesPurchaseEntryMaster objSalesPurchase = _ISalesPurchaseEntry_Repository.GetSalePurchaseEntryByID(id);
                    objSalesPurchase.IsActive = false;
                    objSalesPurchase.DeletedBy = sessionUtils.UserId;
                    objSalesPurchase.DeletedDate = DateTime.Now;

                    int ResponseVal = _ISalesPurchaseEntry_Repository.DeleteSalePurchaseEntry(objSalesPurchase);
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
                ex.SetLog("Delete SalesPurchaseEntry");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllSalesPurchaseEntryById(int id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    SalesPurchaseEntryMaster objSalesPurchaseModel = _ISalesPurchaseEntry_Repository.GetSalePurchaseEntryByID(id);
                    List<SalesPurchaseDocumentMaster> objSalesPurchaseDocMaster = _ISalesPurchaseEntry_Repository.GetSalePurchaseDocBySaleID(id).ToList();

                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objSalesPurchaseModel = objSalesPurchaseModel,
                        objSalesPurchaseDocMaster = objSalesPurchaseDocMaster,
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get SalesPurchase");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    throw ex;
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
    }
}