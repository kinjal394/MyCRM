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

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class TOTypeController : Controller
    {
        private ITOType_Repository _ITOType_Repository;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TOTypePopup()
        {
            return View();
        }
        public TOTypeController()
        {
            this._ITOType_Repository = new TOType_Repository(new elaunch_crmEntities());
        }

        [HttpPost]
        public JsonResult SaveTOType(TOTypeMaster objctTOType)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    TOTypeMaster ObjTOType = new TOTypeMaster();
                    ObjTOType.TOTypeId = objctTOType.TOTypeId;
                    ObjTOType.TOType = objctTOType.TOType;
                    ObjTOType.IsActive = true;
                    if (objctTOType.TOTypeId > 0)
                    {
                        var plist = _ITOType_Repository.DuplicateEditToTypeName(ObjTOType.TOTypeId, ObjTOType.TOType).ToList();
                        if (plist.Count == 0)
                        {
                            _ITOType_Repository.UpdateTOType(ObjTOType);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _ITOType_Repository.DuplicateToType(ObjTOType.TOType).ToList();
                        if (clist.Count == 0)
                        {
                            _ITOType_Repository.AddTOType(ObjTOType);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                            // Already Exists
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
                ex.SetLog("Creat/Update TOType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteTOType(string TOTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (TOTypeId != "")
                    {
                        int cid = Convert.ToInt32(TOTypeId);
                        TOTypeMaster dmaster = new TOTypeMaster();
                        dmaster = _ITOType_Repository.GetTOTypeById(cid);
                        dmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _ITOType_Repository.UpdateTOType(dmaster);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete TOType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTOTypeById(int TOTypeId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objdepart = _ITOType_Repository.GetTOTypeById(TOTypeId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objdepart);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get TOType by ID");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

         [HttpGet]
        public ActionResult TOTypeBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ITOType_Repository.getAllTOType().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All TOType");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ITOType_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}