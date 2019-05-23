using CRM.App_Start;
using CRM.Models;
using CRM.Models.Grid;
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
    public class ReligionController : Controller
    {
        private IReligion_Repository _IReligion_Repository;
        // GET: Master/Religion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReligionPopup()
        {
            return View();
        }
        public ReligionController()

        {
            this._IReligion_Repository = new Religion_Repository(new elaunch_crmEntities());

        }

        public JsonResult GetReligions()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var religionList = _IReligion_Repository.GetAllReligion().ToList();
                return this.Json(religionList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Religion");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SaveReligion(ReligionMaster objReligion)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    ReligionMaster Religion = new ReligionMaster();
                    Religion.ReligionId = objReligion.ReligionId;
                    Religion.ReligionName = objReligion.ReligionName;
                    Religion.IsActive = true;
                    if (objReligion.ReligionId > 0)
                    {
                        var plist = _IReligion_Repository.DuplicateEditReligion(Religion.ReligionId, Religion.ReligionName).ToList();
                        if (plist.Count == 0)
                        {
                            _IReligion_Repository.UpdateReligion(Religion);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Religion Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IReligion_Repository.DuplicateReligion(Religion.ReligionName).ToList();
                        if (clist.Count == 0)
                        {
                            _IReligion_Repository.AddReligion(Religion);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Religion Already Exists", null);

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
                ex.SetLog("Create/Update Religion");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteReligion(string ReligionId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (ReligionId != "")
                    {
                        int cid = Convert.ToInt32(ReligionId);
                        ReligionMaster rmaster = new ReligionMaster();
                        rmaster = _IReligion_Repository.GetReligionById(cid);
                        rmaster.IsActive = false;
                        //smaster.SourceId = cid;
                        _IReligion_Repository.UpdateReligion(rmaster);
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
                ex.SetLog("Delete Religion");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetReligionById(int ReligionId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objReligion = _IReligion_Repository.GetReligionById(ReligionId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objReligion);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Religion by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReligionBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IReligion_Repository.GetAllReligion().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Religion");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                throw ex;
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IReligion_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}