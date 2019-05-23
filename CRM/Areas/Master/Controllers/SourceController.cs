using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM_Repository.Data;
using CRM.Models;
using CRM.App_Start;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class SourceController : Controller
    {
        private ISource_Repository _ISource_Repository;
        // GET: Master/Source
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SourcePopup()
        {
            return View();
        }
        public SourceController()

        {
            this._ISource_Repository = new Source_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SaveSource(SourceMaster objsource)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    SourceMaster source = new SourceMaster();
                    source.SourceId = objsource.SourceId;
                    source.SourceName = objsource.SourceName;
                    source.IsActive = true;
                    if (objsource.SourceId > 0)
                    {
                        var plist = _ISource_Repository.DuplicateEditSource(source.SourceId, source.SourceName).ToList();
                        if (plist.Count == 0)
                        {
                            _ISource_Repository.UpdateSource(source);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Source Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _ISource_Repository.DuplicateSource(source.SourceName).ToList();
                        if (clist.Count == 0)
                        {
                            _ISource_Repository.AddSource(source);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Source Already Exists", null);

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
                ex.SetLog("Create/Update Source");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteSource(string SourceId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (SourceId != "")
                    {
                        int cid = Convert.ToInt32(SourceId);
                        SourceMaster smaster = new SourceMaster();
                        smaster = _ISource_Repository.GetSourceById(cid);
                        smaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _ISource_Repository.UpdateSource(smaster);
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
                ex.SetLog("Delete Source");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSourceById(int SourceId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objship = _ISource_Repository.GetSourceById(SourceId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objship);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Source by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SourceBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ISource_Repository.GetAllSource().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Source");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ISource_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}