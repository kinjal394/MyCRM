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
    public class LegerController : Controller
    {
        private ILeger_Repository _ILeger_Repository;

        public LegerController()
        {
            this._ILeger_Repository = new Leger_Repository(new elaunch_crmEntities());
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LegerPopup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveLeger(LegerMaster objLeger)
        {
            DataResponse dataResponse = new DataResponse();

            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    LegerMaster Legermaster = new LegerMaster();
                    Legermaster.LegerId = objLeger.LegerId;
                    Legermaster.LegerName = objLeger.LegerName;
                    Legermaster.LegerHeadId = objLeger.LegerHeadId;
                    Legermaster.IsActive = true;
                    if (objLeger.LegerId > 0)
                    {
                        var celist = _ILeger_Repository.DuplicateEditLeger(Legermaster.LegerId, Legermaster.LegerName).ToList();
                        if (celist.Count == 0)
                        {
                            _ILeger_Repository.UpdateLeger(Legermaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Leger Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ILeger_Repository.DuplicateLeger(Legermaster.LegerName).ToList();
                        if (clist.Count == 0)
                        {
                            _ILeger_Repository.AddLeger(Legermaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Leger Already Exists", null);

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
                ex.SetLog("Create/Update Leger");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteLeger(string LegerId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (LegerId != "")
                    {
                        int cid = Convert.ToInt32(LegerId);
                        LegerMaster Legermaster = new LegerMaster();
                        Legermaster = _ILeger_Repository.GetLegerById(cid);
                        Legermaster.IsActive = false;
                        _ILeger_Repository.UpdateLeger(Legermaster);
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
                ex.SetLog("Delete Leger");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdLeger(int LegerId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objLeger = _ILeger_Repository.GetLegerById(LegerId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objLeger);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Leger");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ILeger_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}