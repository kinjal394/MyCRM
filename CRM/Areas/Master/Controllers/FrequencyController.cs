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
    public class FrequencyController : Controller
    {
        private IFrequency_Repository _IFrequency_Repository;

        public FrequencyController()
        {
            this._IFrequency_Repository = new Frequency_Repository(new elaunch_crmEntities());
        }
        // GET: Master/Frequency
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFrequency()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveFrequency(FrequencyMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                FrequencyMaster FrequencyObj = new FrequencyMaster();
                FrequencyObj.FrequencyId = d.FrequencyId;
                FrequencyObj.Frequency = d.Frequency.Trim();
                FrequencyObj.IsActive = true;
                if (FrequencyObj.FrequencyId > 0)
                {
                    var cntList = _IFrequency_Repository.DuplicateEditFrequency(FrequencyObj.FrequencyId, FrequencyObj.Frequency).ToList();
                    if (cntList.Count == 0)
                    {
                        _IFrequency_Repository.UpdateFrequency(FrequencyObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Frequency Name Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IFrequency_Repository.DuplicateFrequency(FrequencyObj.Frequency).ToList();
                    if (cntList.Count == 0)
                    {
                        _IFrequency_Repository.AddFrequency(FrequencyObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Frequency Name Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Frequency");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteFrequency(int FrequencyId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                FrequencyMaster Frequencyobj = new FrequencyMaster();
                Frequencyobj = _IFrequency_Repository.GetFrequencyByID(FrequencyId);
                Frequencyobj.IsActive = false;
                _IFrequency_Repository.UpdateFrequency(Frequencyobj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Frequency");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdFrequency(int FrequencyId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IFrequency_Repository.GetFrequencyByID(Convert.ToInt32(FrequencyId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Frequency by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _IFrequency_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}