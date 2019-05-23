using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.App_Start;
using CRM_Repository.Data;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using CRM.Models;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class ReportFormatController : Controller
    {
        private IReportFormat_Repository _IReportFormat_Repository;
        public ReportFormatController()
        {
            this._IReportFormat_Repository = new ReportFormat_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Master/ReportFormat
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddReport()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveReport(ReportFormatMaster d)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                ReportFormatMaster ReportObj = new ReportFormatMaster();
                ReportObj.RotFormatId = d.RotFormatId;
                ReportObj.CompanyCode = d.CompanyCode.Trim();
                ReportObj.CompanyFooter = d.CompanyFooter;
                ReportObj.CompanyHeader = d.CompanyHeader;
                ReportObj.IsActive = true;
                if (ReportObj.RotFormatId > 0)
                {
                    var cntList = _IReportFormat_Repository.DuplicateEditReportFormat(ReportObj.RotFormatId, ReportObj.CompanyCode).ToList();
                    if (cntList.Count == 0)
                    {
                        _IReportFormat_Repository.UpdateReportFormat(ReportObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Company Code Already Exists.", null);
                    }
                }
                else
                {
                    var cntList = _IReportFormat_Repository.DuplicateReportFormat(ReportObj.CompanyCode).ToList();
                    if (cntList.Count == 0)
                    {
                        _IReportFormat_Repository.AddReportFormat(ReportObj);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully.", null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Company Code Already Exists.", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Create/Update Company Code");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteReport(int RotFormatId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //_IAgencyType_Repository.DeleteAgencyType(Convert.ToInt32(AgencyTypeId));
                ReportFormatMaster ReportObj = new ReportFormatMaster();
                ReportObj = _IReportFormat_Repository.GetReportFormatByID(RotFormatId);
                ReportObj.IsActive = false;
                _IReportFormat_Repository.UpdateReportFormat(ReportObj);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully.", null);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Report Format");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdReport(int RotFormatId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var obj = _IReportFormat_Repository.GetReportFormatByID(Convert.ToInt32(RotFormatId));
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, obj);

            }
            catch (Exception ex)
            {
                ex.SetLog("Get Report Format by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IReportFormat_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}