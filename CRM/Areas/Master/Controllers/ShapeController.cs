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
using System.IO;
using System.Web.Hosting;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class ShapeController : Controller
    {
        private IShape_Repository _IShape_Repository;

        public ShapeController()
        {
            this._IShape_Repository = new Shape_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Master/Shape
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddShape()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(ShapeMaster objShape)
        {

            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/"+ConfigurationManager.AppSettings["TempImgPath"]);
            string ShapeImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ShapeImgPath"]);
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    bool isExist = _IShape_Repository.IsExist(objShape.ShapeId, objShape.ShapeName);
                    if (!isExist)
                    {
                        if (objShape.ShapeId <= 0)
                        {
                            objShape.IsActive = true;
                            _IShape_Repository.SaveShape(objShape);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        }
                        else
                        {
                            ShapeMaster objShapeMaster = _IShape_Repository.GetByShapeId(objShape.ShapeId);
                            objShapeMaster.ShapeName = objShape.ShapeName;
                            objShapeMaster.Description = objShape.Description;
                            objShapeMaster.Photo = objShape.Photo;
                            _IShape_Repository.UpdateShape(objShapeMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        }

                        if (System.IO.File.Exists(TempImgPath.ToString()+ "/"+ objShape.Photo.ToString()))
                        {
                            System.IO.File.Move( TempImgPath.ToString() + "/" + objShape.Photo.ToString(), ShapeImgPath.ToString() + "/" + objShape.Photo.ToString());
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Shape Already Exists", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create/Update Shape");
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
        public JsonResult Delete(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _IShape_Repository.DeleteShape(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete Shape");
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
                    ShapeMaster objShape = _IShape_Repository.GetByShapeId(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objShape);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get Shape");
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
            _IShape_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}