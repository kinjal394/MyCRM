using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Employee.Controllers
{
    [HasLoginSessionFilter]
    public class IntervieweeCandidateController : Controller
    {
        private IIntervieweeCandidate_Repository _IIntervieweeCandidate_Repository;
        public IntervieweeCandidateController()
        {
            this._IIntervieweeCandidate_Repository = new IntervieweeCandidate_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }

        // GET: Employee/IntervieweeCandidate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddIntervieweeCandidate(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }

        [HttpPost]
        public JsonResult CreateUpdate(IntervieweeCandidateModel objInterviweeCandidate)
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
                    string ResumePath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ResumePath"]);
                    if (!System.IO.Directory.Exists(ResumePath))
                    {
                        System.IO.Directory.CreateDirectory(ResumePath);
                    }
                    //if (objInterviweeCandidate.IntCandId <= 0)
                    //{
                    //    var intrcanlist = _IIntervieweeCandidate_Repository.GetAllIntervieweeCandidate().LastOrDefault();
                    //    if (intrcanlist.CandidateRefno == objInterviweeCandidate.CandidateRefno)
                    //    {
                    //        var getData = cm.GetAutoNumber("InwardCourier");
                    //        objInterviweeCandidate.CandidateRefno = getData.Rows[0][0].ToString();
                    //    }
                    //}
                    int ResponseVal = _IIntervieweeCandidate_Repository.CreateUpdate(objInterviweeCandidate);
                    //ResponseVal 1: insert,2:update ,0: error
                    if (ResponseVal == 1 || ResponseVal == 2)
                    {
                        if (ResponseVal == 1)
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        if (ResponseVal == 2)
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        if (objInterviweeCandidate.UploadResume != null)
                        {
                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + objInterviweeCandidate.UploadResume.ToString()))
                            {
                                if (!System.IO.File.Exists(ResumePath.ToString() + "/" + objInterviweeCandidate.UploadResume.ToString()))
                                {
                                    System.IO.File.Move(TempImgPath.ToString() + "/" + objInterviweeCandidate.UploadResume.ToString(), ResumePath.ToString() + "/" + objInterviweeCandidate.UploadResume.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Opps! something wrong", null);
                    }
                }
                catch (Exception ex)
                {
                    ex.SetLog("Create Interviewee Candidate");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    _IIntervieweeCandidate_Repository.Delete(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                }
                catch (Exception ex)
                {
                    ex.SetLog("Delete InwardCourier");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "User is not valid", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InterviewCanInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("InterviweeCandidate");
                var data = new
                {
                    CandidateRefno = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get InterviewCanInfo");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult FetchAllInfoById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            if (sessionUtils.HasUserLogin())
            {
                try
                {
                    IntervieweeCandidateModel objInterviweeCandidate = _IIntervieweeCandidate_Repository.GetIntervieweeCandidateById(Id);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", new
                    {
                        objInterviweeCandidate = objInterviweeCandidate
                    });
                }
                catch (Exception ex)
                {
                    ex.SetLog("Get FetchInfo in Interviewee Candidate");
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                    throw;
                }
            }
            else
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.InvalidUser, "Invalid User", null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IIntervieweeCandidate_Repository.Dispose();
            base.Dispose(disposing);
        }

    }
}