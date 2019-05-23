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
    public class CountryOriginController : Controller
    {
        private ICountryOrigin_Repository _ICountryOrigin_Repository;

        public CountryOriginController()
        {
            this._ICountryOrigin_Repository = new CountryOrigin_Repository(new elaunch_crmEntities());
        }
        // GET: Master/CountryOrigin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CountryOriginPopup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveCountryOrigin(CountryOfOriginMaster objorigin)
        {
            DataResponse dataResponse = new DataResponse();

            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    CountryOfOriginMaster cmaster = new CountryOfOriginMaster();
                    cmaster.OriginId = objorigin.OriginId;
                    cmaster.CountryOfOrigin = objorigin.CountryOfOrigin;
                    cmaster.CountryId = objorigin.CountryId;
                    cmaster.IsActive = true;
                    if (objorigin.OriginId > 0)
                    {
                        var celist = _ICountryOrigin_Repository.DuplicateEditOrigin(cmaster.OriginId, cmaster.CountryOfOrigin).ToList();
                        if (celist.Count == 0)
                        {
                            _ICountryOrigin_Repository.UpdateOrigin(cmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "CountryIrigin Name Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ICountryOrigin_Repository.DuplicateOrigin(cmaster.CountryOfOrigin).ToList();
                        if (clist.Count == 0)
                        {
                            _ICountryOrigin_Repository.InsertOrigin(cmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "CountryIrigin Name Already Exists", null);

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
                ex.SetLog("Create/Update CountryOrigin");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCountryOrigin(string OriginId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (OriginId != "")
                    {
                        int cid = Convert.ToInt32(OriginId);
                        CountryOfOriginMaster cmaster = new CountryOfOriginMaster();
                        cmaster = _ICountryOrigin_Repository.GetOriginID(cid);
                        cmaster.IsActive = false;
                        _ICountryOrigin_Repository.UpdateOrigin(cmaster);
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
                ex.SetLog("Delete CountryOrigin");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdCountryOrigin(int OriginID)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objcountry = _ICountryOrigin_Repository.GetOriginID(OriginID);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objcountry);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get CountryOrigin by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ICountryOrigin_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}