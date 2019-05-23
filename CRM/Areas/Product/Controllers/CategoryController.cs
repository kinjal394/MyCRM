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
    public class CategoryController : Controller
    {
        private ICategory_Repository _ICategory_Repository;
        private ISubCategory_Repository _ISubCategory_Repository;

        public CategoryController()
        {
            this._ICategory_Repository = new Category_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ISubCategory_Repository = new SubCategory_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(CategoryMaster objCategory)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (objCategory.CategoryId <= 0)
                    {
                        //add
                        var Isrepeat = _ICategory_Repository.IsCategoryRepeat(objCategory).ToList();
                        if (Isrepeat.Count <= 0)
                        {
                            objCategory.CreatedBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                            objCategory.CreatedDate = DateTime.Now;
                            objCategory.IsActive = true;
                            _ICategory_Repository.SaveCategory(objCategory);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Duplicate Category not allowed", null);
                        }

                    }
                    else
                    {
                        var Isrepeat = _ICategory_Repository.IsCategoryRepeat(objCategory).ToList();
                        if (Isrepeat.Count <= 0)
                        {
                            //edit
                            CategoryMaster objCategoryInfo = _ICategory_Repository.GetCategoryById(objCategory.CategoryId);
                            objCategoryInfo.CategoryName = objCategory.CategoryName;
                            objCategoryInfo.ModifyBy = sessionUtils.UserId;// Convert.ToInt32(Session["UserId"]);
                            objCategoryInfo.ModifyDate = DateTime.Now;
                            _ICategory_Repository.UpdateCategory(objCategoryInfo);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Duplicate Category not allowed", null);
                        }

                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Category");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //DONE
        [HttpPost]
        public JsonResult DeleteCategory(int CategoryId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    var subcat = _ISubCategory_Repository.GetSubCategorybyCategory(CategoryId).ToList();
                    if (subcat.Count == 0)
                    {
                        _ICategory_Repository.DeleteCategory(UserId, Convert.ToInt32(CategoryId));
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "This Category use in Subcategory so,Delete SubCategory then Delete Category.", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Category");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        //done
        [HttpGet]
        public JsonResult GetCategoryById(int CategoryId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objCategory = _ICategory_Repository.GetCategoryById(CategoryId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objCategory);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Category by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetCategory()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                List<CategoryMaster> data = _ICategory_Repository.GetCategory().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Category");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

    }
}