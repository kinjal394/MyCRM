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
    public class SubCategoryController : Controller
    {
        private ISubCategory_Repository _ISubCategory_Repository;
        private IProduct_Repository _IProduct_Repository;
        public SubCategoryController()
        {
            this._ISubCategory_Repository = new SubCategory_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IProduct_Repository = new Product_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Product/SubCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSubCategory()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(SubCategoryMaster SubCategory)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {

                    if (SubCategory.SubCategoryId <= 0)
                    {
                        bool isExist = _ISubCategory_Repository.IsExist(SubCategory, "ADD");
                        if (!isExist)
                        {
                            SubCategory.CreatedBy = sessionUtils.UserId;
                            SubCategory.CreatedDate = DateTime.Now;
                            SubCategory.IsActive = true;
                            _ISubCategory_Repository.SaveSubCategory(SubCategory);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "SubCategory Already Exists", null);
                        }
                    }
                    else
                    {
                        bool isExist = _ISubCategory_Repository.IsExist(SubCategory, "EDIT");
                        if (!isExist)
                        {
                            SubCategoryMaster objSubCategoryMaster = _ISubCategory_Repository.GetSubCategorybyId(SubCategory.SubCategoryId);
                            objSubCategoryMaster.SubCategoryId = SubCategory.SubCategoryId;
                            objSubCategoryMaster.SubCategoryName = SubCategory.SubCategoryName;
                            objSubCategoryMaster.CategoryId = SubCategory.CategoryId;
                            objSubCategoryMaster.ModifyBy = sessionUtils.UserId;
                            objSubCategoryMaster.ModifyDate = DateTime.Now;
                            _ISubCategory_Repository.UpdateSubCategory(objSubCategoryMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "SubCategory Already Exists", null);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update SubCategory");
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
        public JsonResult DeleteSubCategory(int SubCategoryId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (Session["UserId"] != null)
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    var mainprod = _IProduct_Repository.GetProductBySubCategoryId(SubCategoryId).ToList();
                    if (mainprod.Count == 0)
                    {
                        _ISubCategory_Repository.DeleteSubCategory(Convert.ToInt32(SubCategoryId), UserId);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Deleted Succesfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "This Subcategory use in MainProduct so,Delete MainProduct then Delete Subcategory.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete SubCategory");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetSubCategory()
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    try
        //    {
        //        List<SubCategoryMaster> data = _ISubCategory_Repository.GetSubCategory().ToList();
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetSubCategoryById(int SubcategoryId)
        //{
        //    DataResponse dataResponse = new DataResponse();
        //    try
        //    {
        //        var data = _ISubCategory_Repository.GetSubCategorybyId(SubcategoryId);
        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult GetSubCategorybyCategory(int categoryid)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<SubCategoryMaster> data = _ISubCategory_Repository.GetSubCategorybyCategory(categoryid).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get SubCategory");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Something Wrong", null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

    }
}