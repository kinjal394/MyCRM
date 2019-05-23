using CRM.App_Start;
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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class PlugShapeController : Controller
    {

        private IPlugShape_Repository _IPlugShape_Repository;

        public PlugShapeController()
        {
            this._IPlugShape_Repository = new PlugShape_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Master/PlugShape
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPlugShape()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(PlugShapeMaster objPlugShape)
        {
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string PlugShapeImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["PlugShapeImgPath"]);
            DataResponse dataResponse = new DataResponse();

            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    bool isExist = _IPlugShape_Repository.IsExist(objPlugShape.PlugShapeId, objPlugShape.Title);
                    if (!isExist)
                    {
                        if (objPlugShape.PlugShapeId <= 0)
                        {
                            objPlugShape.IsActive = true;
                            _IPlugShape_Repository.SavePlugShape(objPlugShape);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {

                            PlugShapeMaster objPlugShapeMaster = _IPlugShape_Repository.GetByPlugShapeId(objPlugShape.PlugShapeId);
                            objPlugShapeMaster.Title = objPlugShape.Title;
                            objPlugShapeMaster.Description = objPlugShape.Description;
                            objPlugShapeMaster.Photo = objPlugShape.Photo;
                            _IPlugShape_Repository.UpdatePlugShape(objPlugShapeMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objPlugShape.Photo.ToString()))
                        {
                            System.IO.File.Move(TempImgPath.ToString() + "/" + objPlugShape.Photo.ToString(), PlugShapeImgPath.ToString() + "/" + objPlugShape.Photo.ToString());
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "PlugShape Already Exists", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update PlugShape");
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
        public JsonResult Delete(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _IPlugShape_Repository.DeletePlugShape(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete PlugShape");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    PlugShapeMaster objPlugShape = _IPlugShape_Repository.GetByPlugShapeId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objPlugShape);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get PlugShape");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }

            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IPlugShape_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}