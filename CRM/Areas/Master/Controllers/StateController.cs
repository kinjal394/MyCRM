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
    public class StateController : Controller
    {
        private IState_Repository _IState_Repository;
        private ICountry_Repository _ICountry_Repository;
        public StateController()
        {
            this._IState_Repository = new State_Repository(new elaunch_crmEntities());
            this._ICountry_Repository = new Country_Repository(new elaunch_crmEntities());
        }

        // GET: Master/State
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StatePopup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveState(StateMaster objstate)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    StateMaster objState = new StateMaster();
                    objState.CountryId = objstate.CountryId;
                    objState.StateId = objstate.StateId;
                    objState.StateName = objstate.StateName;
                    objState.IsActive = true;
                    if (objstate.StateId > 0)
                    {
                        var celist = _IState_Repository.CheckForDuplicateState(objState.StateId, objState.StateName).ToList();
                        if (celist.Count == 0)
                        {
                            _IState_Repository.UpdateState(objState);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "State Already Exists", null);

                        }
                    }
                    else
                    {
                        var clist = _IState_Repository.CheckForDuplicateState(objState.StateId, objState.StateName).ToList();
                        if (clist.Count == 0)
                        {
                            _IState_Repository.InsertState(objState);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "State Already Exists", null);

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
                ex.SetLog("Create/Update State");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteState(string StateId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (StateId != "")
                    {
                        int sid = Convert.ToInt32(StateId);
                        StateMaster smaster = new StateMaster();
                        smaster = _IState_Repository.GetStateByID(sid);
                        smaster.IsActive = false;
                        _IState_Repository.UpdateState(smaster);
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
                ex.SetLog("Delete State");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdState(int StateID)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                //if (sessionUtils.HasUserLogin())
                //{
                    var objstate = _IState_Repository.GetStateByID(StateID);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objstate);
                //}
                //else
                //{
                //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                //}
            }
            catch (Exception ex)
            {
                ex.SetLog("Get State by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult StateBind(int CountryId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IState_Repository.GetStateByCountryID(CountryId).ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All State");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _ICountry_Repository.Dispose();
            _IState_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}