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
    public class LegerHeadController : Controller
    {
        private ILegerHead_Repository _ILegerHead_Repository;

        public LegerHeadController()
        {
            this._ILegerHead_Repository = new LegerHead_Repository(new elaunch_crmEntities());
        }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LegerHeadPopup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveLegerHead(LegerHeadMaster objLHead)
        {
            DataResponse dataResponse = new DataResponse();

            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    LegerHeadMaster LHeadmaster = new LegerHeadMaster();
                    LHeadmaster.LegerHeadId = objLHead.LegerHeadId;
                    LHeadmaster.LegerHeadName = objLHead.LegerHeadName;
                    LHeadmaster.ITRId = objLHead.ITRId;
                    LHeadmaster.IsActive = true;
                    if (objLHead.LegerHeadId > 0)
                    {
                        var celist = _ILegerHead_Repository.DuplicateEditLegerHead(LHeadmaster.LegerHeadId, LHeadmaster.LegerHeadName).ToList();
                        if (celist.Count == 0)
                        {
                            _ILegerHead_Repository.UpdateLegerHead(LHeadmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Leger Head Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _ILegerHead_Repository.DuplicateLegerHead(LHeadmaster.LegerHeadName).ToList();
                        if (clist.Count == 0)
                        {
                            _ILegerHead_Repository.AddLegerHead(LHeadmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Leger Head Already Exists", null);

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
                ex.SetLog("Create/Update LegerHead");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteLegerHead(string LegerHeadId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (LegerHeadId != "")
                    {
                        int cid = Convert.ToInt32(LegerHeadId);
                        LegerHeadMaster Lmaster = new LegerHeadMaster();
                        Lmaster = _ILegerHead_Repository.GetLegerHeadById(cid);
                        Lmaster.IsActive = false;
                        _ILegerHead_Repository.UpdateLegerHead(Lmaster);
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
                ex.SetLog("Delete LegerHead");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdLegerHead(int LegerHeadId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objLegerHead = _ILegerHead_Repository.GetLegerHeadById(LegerHeadId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objLegerHead);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get LegerHead");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _ILegerHead_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}