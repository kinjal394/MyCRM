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
    public class AdvertisementSourceController : Controller
    {
        private IAdvertisementSource_Repository _IAdvertisementSource_Repository;
        // GET: Master/AdvertisementSource
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdvertisementSourcePopup()
        {
            return View();
        }
        public AdvertisementSourceController()

        {
            this._IAdvertisementSource_Repository = new AdvertisementSource_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SaveAdvertSource(AdvertisementSourceMaster objAdvertSource)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    AdvertisementSourceMaster advsource = new AdvertisementSourceMaster();
                    advsource = objAdvertSource;
                    advsource.IsActive = true;
                    if (objAdvertSource.SiteId > 0)
                    {
                        var plist = _IAdvertisementSource_Repository.DuplicateEditAdvertSource(advsource.SiteId, advsource.SiteName).ToList();
                        if (plist.Count == 0)
                        {
                            _IAdvertisementSource_Repository.UpdateAdvertSource(advsource);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "AdvertisementSource Name Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IAdvertisementSource_Repository.DuplicateAdvertSource(advsource.SiteName).ToList();
                        if (clist.Count == 0)
                        {
                            _IAdvertisementSource_Repository.AddAdvertSource(advsource);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "AdvertisementSource Name Already Exists", null);

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
                ex.SetLog("Create/Update Advertisement Source");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteAdvertSource(string SiteId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (SiteId != "")
                    {
                        int cid = Convert.ToInt32(SiteId);
                        AdvertisementSourceMaster asmaster = new AdvertisementSourceMaster();
                        asmaster = _IAdvertisementSource_Repository.GetAdvertSourceById(cid);
                        asmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _IAdvertisementSource_Repository.UpdateAdvertSource(asmaster);
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
                ex.SetLog("Delete Advertisement Source");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAdvertSourceById(int SiteId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objadvsource = _IAdvertisementSource_Repository.GetAdvertSourceById(SiteId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objadvsource);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Advertisement Source by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IAdvertisementSource_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}