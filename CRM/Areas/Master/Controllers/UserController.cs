using CRM.App_Start;
using CRM.Models;
using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    [HasLoginSessionFilter]
    public class UserController : Controller
    {
        private IUser_Repository _IUser_Repository;
        private IUserContactDetail_Repository _IUserContactDetail_Repository;
        private ICountry_Repository _ICountry_Repository;
        private IDepartment_Repository _IDepartment_Repository;
        private IRole_Repository _IRole_Repository;
        private IUserRefferenceDetail_Repository _IUserRefferenceDetail_Repository;
        EncryptDecrypt _Encdcrpt = new EncryptDecrypt();

        public UserController()
        {
            this._IUser_Repository = new User_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IUserContactDetail_Repository = new UserContactDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IDepartment_Repository = new Department_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._ICountry_Repository = new Country_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IRole_Repository = new Role_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IUserRefferenceDetail_Repository = new UserRefferenceDetail_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        // GET: Master/User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserPopup(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            ViewBag.usertype = Session["UserRollType"].ToString();
            //ViewBag.Mode = mode;
            return View();
        }
        public ActionResult EditUser(int id = 0, string mode = "edit")
        {
            ViewBag.id = id;
            ViewBag.Mode = mode;
            return View();
        }
        public ActionResult UserFamilyPopup()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveUser(UserMaster obj)
        {
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string UserImagePath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["UserImagePath"]);
            string EmpDocumentImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["EmpDocumentImgPath"]);
            if (!System.IO.Directory.Exists(UserImagePath))
            {
                System.IO.Directory.CreateDirectory(UserImagePath);
            }
            if (!System.IO.Directory.Exists(EmpDocumentImgPath))
            {
                System.IO.Directory.CreateDirectory(EmpDocumentImgPath);
            }
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                //if (obj.UserContactDetails != null && obj.UserContactDetails.Count >= 4)
                //{
                string str = _Encdcrpt.Encypt("123456").ToString();
                string ReportingId = Convert.ToString(obj.ReportingId);
                //var isadd = true;
                //if(obj.UserContactDetails == null || obj.UserContactDetails.Count < 4)
                //{
                //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Enter Minmum 4 Family Contact Details.", null);
                //    isadd = false;
                //}
                //else if (obj.UserReferanceDetails == null || obj.UserReferanceDetails.Count < 4)
                //{
                //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Enter Minmum 4 Reference Details.", null);
                //    isadd = false;
                //}
                //if (isadd == true)
                //{
                if (sessionUtils.HasUserLogin())
                {
                    UserMaster user = new UserMaster();
                    user = obj;
                    user.UserName = obj.Email;
                    user.Password = str;
                    user.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    user.CreatedDate = DateTime.Now;
                    user.ModifyBy = Convert.ToInt32(Session["UserId"]);
                    user.ModifyDate = DateTime.Now;
                    user.IsActive = true;
                    //user.UserId = obj.UserId;
                    if (obj.UserId > 0)
                    {
                        //if (user.UserName != null)
                        //{
                        //    var celist = _IUser_Repository.DuplicateEditUser(user.UserId, user.UserName).ToList();
                        //    if (celist.Count == 0)
                        //    {
                        //        _IUser_Repository.UpdateUser(user);
                        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", null);
                        //    }
                        //    else
                        //    {
                        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "User Email Already Exists", null);
                        //    }
                        //}
                        //else
                        //{
                        UserMaster objuser = _IUser_Repository.UpdateUser(user);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Update successfully", objuser.UserId);
                        //}

                    }
                    else
                    {
                        var userlist = _IUser_Repository.getAllUser().LastOrDefault();
                        if (userlist.UserCode == user.UserCode)
                        {
                            var getData = cm.GetAutoNumber("EMP");
                            user.UserCode = getData.Rows[0][0].ToString();
                        }
                        user.Password = str;
                        //if (user.UserName != null)
                        //{
                        //    var celist = _IUser_Repository.DuplicateUser(user.UserName).ToList();
                        //    if (celist.Count == 0)
                        //    {
                        //        _IUser_Repository.AddUser(user);
                        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", null);
                        //    }
                        //    else
                        //    {
                        //        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "User Email Already Exists", null);
                        //    }
                        //}
                        //else
                        //{
                        UserMaster objuser = _IUser_Repository.AddUser(user);
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "Insert successfully", objuser.UserId);
                        //}
                    }
                    if (obj.Photo != null && obj.Photo != "")
                    {
                        if (System.IO.File.Exists(TempImgPath.ToString() + "/" + obj.Photo.ToString()))
                        {
                            System.IO.File.Move(TempImgPath.ToString() + "/" + obj.Photo.ToString(), UserImagePath.ToString() + "/" + obj.Photo.ToString());
                        }
                    }
                    if (obj.UserDocumentDetails != null)
                    {
                        foreach (var item in obj.UserDocumentDetails)
                        {
                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.DocUpload.ToString()))
                            {
                                System.IO.File.Move(TempImgPath.ToString() + "/" + item.DocUpload.ToString(), EmpDocumentImgPath.ToString() + "/" + item.DocUpload.ToString());
                            }
                        }
                    }
                }
                else
                {
                    //return PartialView("AddBank", obj);
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Param, null);
                }

                //}
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "Enter Minmum 4 Family Contact Details.", null);
                //    return Json(dataResponse, JsonRequestBehavior.AllowGet);
                //}
            }

            catch (Exception ex)
            {
                ex.SetLog("Create/Update User");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, MessageValue.Error, null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteUser(string UserId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (UserId != "")
                    {
                        int sid = Convert.ToInt32(UserId);
                        UserMaster user = new UserMaster();
                        user = _IUser_Repository.GetUserById(sid);
                        user.IsActive = false;
                        user.DeletedBy = Convert.ToInt32(Session["UserId"]);
                        user.DeletedDate = DateTime.Now;
                        _IUser_Repository.UpdateUser(user);
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
                ex.SetLog("Delete User");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActiveInActiveStatus(string UserId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (UserId != "")
                    {
                        int sid = Convert.ToInt32(UserId);
                        UserMaster user = new UserMaster();
                        user = _IUser_Repository.getAllUser().Where(x => x.UserId == sid).FirstOrDefault();
                        if (user.IsActive == true)
                        {
                            user.IsActive = false;
                            user.DeletedBy = Convert.ToInt32(Session["UserId"]);
                            user.DeletedDate = DateTime.Now;
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "User Successfully De-Active.", null);
                        }
                        else
                        {
                            user.IsActive = true;
                            user.ModifyBy = Convert.ToInt32(Session["UserId"]);
                            user.ModifyDate = DateTime.Now;
                            dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "User Successfully Active.", null);
                        }
                        _IUser_Repository.UpdateUser(user);
                    }
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Activestatus User");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserById(int UserId)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    var objuser = _IUser_Repository.GetuserById(UserId).ToList();
                    List<UserReferenceRelationMaster> objUserContactDetail = _IUserContactDetail_Repository.GetUserContactbyid(UserId).ToList();
                    List<UserRefferenceDetail> UserReferanceDetail = _IUserRefferenceDetail_Repository.GetUserRefferencebyuserid(UserId).ToList();
                    List<UserSalaryDetail> UserSalaryDetail = _IUserContactDetail_Repository.GetUserSalarybyid(UserId).ToList();
                    List<UserDocDetail> UserDocumentDetail = _IUserContactDetail_Repository.GetUserDocbyid(UserId).ToList();
                    List<UserExperienceDetail> UserExperDetails = _IUserContactDetail_Repository.GetUserExperbyid(UserId).ToList();
                    List<UserEducationDetail> UserEducationDetails = _IUserContactDetail_Repository.GetUserEducationid(UserId).ToList();
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, string.Empty, new
                    {
                        UserDetail = objuser,
                        UserContactDetail = objUserContactDetail,
                        UserReferanceDetail = UserReferanceDetail,
                        UserSalaryDetail = UserSalaryDetail,
                        UserDocumentDetail = UserDocumentDetail,
                        UserExperDetails = UserExperDetails,
                        UserEducationDetails = UserEducationDetails
                    });
                }
                else
                {
                    dataResponse = CRMUtilities.GenerateApiResponse(MessageType.NoDataFound, "User is not valid", null);
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Get User");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MobCodesBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var MobCodesList = _ICountry_Repository.GetAllCountry().ToList();
                return this.Json(MobCodesList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get MobCode");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult DepartmentBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var DepartmenList = _IDepartment_Repository.getAllDepartment().ToList();

                return this.Json(DepartmenList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UserInfo()
        {
            DataResponse dataResponse = new DataResponse();
            Common cm = new Common();
            try
            {
                var getData = cm.GetAutoNumber("EMP");
                var data = new
                {
                    UserCode = getData.Rows[0][0].ToString()
                };
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Suceess, "", data);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get UserInfo");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoleBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var RoleList = _IRole_Repository.GetAllRole().ToList();

                return this.Json(RoleList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Role");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult CheckUser(UserMaster obj)
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                if (obj.Email != null)
                {
                    var celist = _IUser_Repository.DuplicateEditUser(obj.UserId, obj.Email).ToList();
                    if (celist.Count > 0)
                    {
                        dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, "User Email Already Exists", null);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.SetLog("Check User");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
            }
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ReportingBind()
        {
            DataResponse dataResponse = new DataResponse();
            try
            {
                var ReportingList = _IUser_Repository.getAllUser().ToList();
                //JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                //var result = JsonConvert.SerializeObject(ReportingList, Formatting.Indented, jss);
                return this.Json(ReportingList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.SetLog("Get Reporting");
                dataResponse = CRMUtilities.GenerateApiResponse(MessageType.Error, ex.InnerException.ToString(), null);
                return Json(dataResponse, JsonRequestBehavior.AllowGet);
            }

        }
        protected override void Dispose(bool disposing)
        {
            _ICountry_Repository.Dispose();
            _IDepartment_Repository.Dispose();
            _IRole_Repository.Dispose();
            _IUserContactDetail_Repository.Dispose();
            _IUser_Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}