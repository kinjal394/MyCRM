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
    public class PercentageController : Controller
    {
        private IPercentage_Repository _IPercentage_Repository;
        // GET: Master/Percentage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PercentagePopup()
        {
            return View();
        }
        public PercentageController()

        {
            this._IPercentage_Repository = new Percentage_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SavePercentage(PercentageMaster objPercentage)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    PercentageMaster Percent = new PercentageMaster();
                    Percent = objPercentage;
                    Percent.IsActive = true;
                    if (objPercentage.PercentageId > 0)
                    {
                        //var plist = _IPercentage_Repository.DuplicateEditPercentage(Percent.PercentageId, Percent.Percentage).ToList();
                        //if (plist.Count == 0)
                        //{
                            _IPercentage_Repository.UpdatePercentage(Percent);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        //}
                        //else
                        //{
                        //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                        //}
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        //var clist = _IPercentage_Repository.DuplicatePercentage(Percent.Percentage).ToList();
                        //if (clist.Count == 0)
                        //{
                            _IPercentage_Repository.AddPercentage(Percent);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        //}
                        //else
                        //{
                        //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Already Exists", null);

                        //    // Already Exists
                        //}
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Percentage");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeletePercentage(string PercentageId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (PercentageId != "")
                    {
                        int cid = Convert.ToInt32(PercentageId);
                        PercentageMaster pmaster = new PercentageMaster();
                        pmaster = _IPercentage_Repository.GetPercentageById(cid);
                        pmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _IPercentage_Repository.UpdatePercentage(pmaster);
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
                ex.SetLog("Delete Percentage");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPercentageById(int PercentageId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objpercent = _IPercentage_Repository.GetPercentageById(PercentageId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objpercent);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Percentage");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IPercentage_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}