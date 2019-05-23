using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM.Models;
using System.Data.Entity.Validation;
using CRM.App_Start;
using System.Web.Hosting;
using System.Configuration;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class CompanyController : Controller
    {
        private ICompany_Repository _ICompany_Repository;

        public CompanyController()
        {
            this._ICompany_Repository = new Company_Repository(new elaunch_crmEntities());

        }

        // GET: Master/Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCompany()
        {
            return View();
        }

        public JsonResult GetCompanyById(int Id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var data = _ICompany_Repository.GetComapnybyid(Id);
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Company by Id");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveCompany(string ComId, string ComCode, string ComName, string RegOffAdd, string CorpOffAdd, string TelNos, string Email, string Web, string ComLogo, string TaxDetails, HttpPostedFileBase file)
        {
            DataResponse dataResponse = new DataResponse();
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string CompanyLogo = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ComLogoImgPath"]);
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyMaster companyobj = new CompanyMaster();
                    companyobj.ComName = ComName;
                    companyobj.RegOffAdd = RegOffAdd;
                    companyobj.CorpOffAdd = CorpOffAdd;
                    companyobj.TelNos = TelNos;
                    companyobj.Email = Email;
                    companyobj.Web = Web;
                    companyobj.ComCode = ComCode;
                    companyobj.ComLogo = ComLogo.Trim();
                    companyobj.TaxDetails = TaxDetails;
                    companyobj.CreatedBy = sessionUtils.UserId;
                    companyobj.CreatedDate = DateTime.Now;
                    companyobj.IsActive = true;
                    if (!_ICompany_Repository.CheckComapnyExist(companyobj, false))
                    {
                        _ICompany_Repository.AddComapny(companyobj);

                        if (!System.IO.Directory.Exists(CompanyLogo))
                        {
                            System.IO.Directory.CreateDirectory(CompanyLogo);
                        }

                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + ComLogo.ToString()))
                        {
                            if (!System.IO.File.Exists(CompanyLogo.ToString() + "/" + ComLogo.ToString()))
                            {
                                System.IO.File.Move(TempImgPath.ToString() + "/" + ComLogo.ToString(), CompanyLogo.ToString() + "/" + ComLogo.ToString());
                            }
                        }
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Insert, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Exists, "Company Name" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Create Company");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteCompany(string id)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                CompanyMaster Compobj = _ICompany_Repository.GetComapnybyid(Convert.ToInt32(id));
                if (Compobj != null)
                {

                    Compobj.IsActive = false;
                    Compobj.DeletedBy = sessionUtils.UserId;
                    Compobj.DeletedDate = DateTime.Now;
                    _ICompany_Repository.UpdateCompany(Compobj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Delete, null);
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Delete Company");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateCompany(string ComId, string ComCode, string ComName, string RegOffAdd, string CorpOffAdd, string TelNos, string Email, string Web, string ComLogo, string TaxDetails, HttpPostedFileBase file)
        {
            string filename = "";
            int CId = int.Parse(ComId);
            Common cs = new Common();
            DataResponse dataResponse = new DataResponse();

            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string CompanyLogo = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ComLogoImgPath"]);

            try
            {
                CompanyMaster obj = _ICompany_Repository.GetComapnybyid(CId);
                if (ModelState.IsValid)
                {
                    obj.ComName = ComName;
                    obj.RegOffAdd = RegOffAdd;
                    obj.CorpOffAdd = CorpOffAdd;
                    obj.TelNos = TelNos;
                    obj.Email = Email;
                    obj.Web = Web;
                    obj.ComCode = ComCode;
                    if (file != null)
                    {
                        filename = cs.fileupload(file, "UploadImages/Companylogo", 32, 32);
                        obj.ComLogo = filename.Trim();
                    }
                    else
                    {
                        var image = obj.ComLogo.Split('/');
                        obj.ComLogo = image[0].Trim();
                    }
                    obj.TaxDetails = TaxDetails;
                    obj.ModifyBy = sessionUtils.UserId;
                    obj.ModifyDate = DateTime.Now;
                    if (!_ICompany_Repository.CheckComapnyExist(obj, true))
                    {
                        _ICompany_Repository.UpdateCompany(obj);
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + ComLogo.ToString()))
                        {
                            if (!System.IO.File.Exists(CompanyLogo.ToString() + "/" + ComLogo.ToString()))
                            {
                                System.IO.File.Move(TempImgPath.ToString() + "/" + ComLogo.ToString(), CompanyLogo.ToString() + "/" + ComLogo.ToString());
                            }
                        }
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, MessageValue.Update, null);
                    }
                    else
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "Company Name" + MessageValue.Exists, null);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }
                return Json(dataResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ex.SetLog("Update Company");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _ICompany_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}