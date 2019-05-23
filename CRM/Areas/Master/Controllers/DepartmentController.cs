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
    public class DepartmentController : Controller
    {
        private IDepartment_Repository _IDepartment_Repository;
        // GET: Master/Department
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DepartmentPopup()
        {
            return View();
        }
        public DepartmentController()

        {
            this._IDepartment_Repository = new Department_Repository(new elaunch_crmEntities());

        }
        [HttpPost]
        public JsonResult SaveDepartment(DepartmentMaster objdepart)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    DepartmentMaster depart = new DepartmentMaster();
                    depart.DepartmentId = objdepart.DepartmentId;
                    depart.DepartmentName = objdepart.DepartmentName;
                    depart.IsActive = true;
                    if (objdepart.DepartmentId > 0)
                    {
                        var plist = _IDepartment_Repository.DuplicateEditDepartment(depart.DepartmentId, depart.DepartmentName).ToList();
                        if (plist.Count == 0)
                        {
                            _IDepartment_Repository.UpdateDepartment(depart);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Department Already Exists", null);

                        }
                        //_ISource_Repository.UpdateSource(source);
                        //msg = "Success";
                    }
                    else
                    {
                        var clist = _IDepartment_Repository.DuplicateDepartment(depart.DepartmentName).ToList();
                         if (clist.Count == 0)
                        {
                            _IDepartment_Repository.AddDepartment(depart);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);

                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Department Already Exists", null);

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
                ex.SetLog("Create/Update Department");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteDepartment(string DepartmentId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (DepartmentId != "")
                    {
                        int cid = Convert.ToInt32(DepartmentId);
                        var dept = _IDepartment_Repository.GetUserbyDepartment(cid).ToList();
                        if (dept.Count == 0)
                        {
                            DepartmentMaster dmaster = new DepartmentMaster();
                            dmaster = _IDepartment_Repository.GetDepartmentById(cid);
                            dmaster.IsActive = false;
                            //smaster.SourceId = cid;
                            _IDepartment_Repository.UpdateDepartment(dmaster);
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Delete successfully", null);
                        }
                        else
                        {
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "This Department use in Employee Detail so, Delete Employee Detail then Delete Department.", null);
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
                ex.SetLog("Delete Department");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartmentById(int DepartmentId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objdepart = _IDepartment_Repository.GetDepartmentById(DepartmentId);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, objdepart);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Department by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DepartmentBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _IDepartment_Repository.getAllDepartment().ToList();
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get All Department");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _IDepartment_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}