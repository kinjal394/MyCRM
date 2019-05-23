using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    public class ReferenceSourceController : Controller
    {

        private IReferenceSourceMaster_Repository _IReferenceSourceMaster_Repository;

        public ReferenceSourceController()
        {
            this._IReferenceSourceMaster_Repository = new ReferenceSourceMaster_Repository(new elaunch_crmEntities());
        }

        // GET: Master/ReferenceSource
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReferenceSourcePopup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdateReferenceSource(ReferenceSourceMaster objReferenceSourceMaster)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {


                    if (objReferenceSourceMaster.SourceId > 0)
                    {
                        var cntSource = _IReferenceSourceMaster_Repository.CheckForDuplicate(objReferenceSourceMaster.SourceId, objReferenceSourceMaster.SourceName).ToList();
                        if (cntSource.Count == 0)
                        {
                            ReferenceSourceMaster  objEditSource = _IReferenceSourceMaster_Repository.GetByID(objReferenceSourceMaster.SourceId);
                            objEditSource.SourceName = objReferenceSourceMaster.SourceName;
                            _IReferenceSourceMaster_Repository.Update(objEditSource);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Source Name Already Exists", null);

                        }
                    }
                    else
                    {
                        var cntSource = _IReferenceSourceMaster_Repository.CheckForDuplicate(objReferenceSourceMaster.SourceId, objReferenceSourceMaster.SourceName).ToList();
                        if (cntSource.Count == 0)
                        {
                            objReferenceSourceMaster.IsActive = true;
                            _IReferenceSourceMaster_Repository.Add(objReferenceSourceMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Source Name Already Exists", null);
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
        public JsonResult DeleteReferenceSource(int SourceId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (SourceId != 0)
                    {
                        ReferenceSourceMaster objReferenceSourceMaster = _IReferenceSourceMaster_Repository.GetByID(SourceId);
                        if (objReferenceSourceMaster != null)
                        {
                            objReferenceSourceMaster.IsActive = false;
                            _IReferenceSourceMaster_Repository.Update(objReferenceSourceMaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                        }
                        else {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Record Not Found..!", null);
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
                ex.SetLog("Delete City");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReferenceSourceById(int SourceId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objcity = _IReferenceSourceMaster_Repository.GetByID(SourceId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objcity);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Reference Source by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IReferenceSourceMaster_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}