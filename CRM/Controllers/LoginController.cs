using CRM_Repository.DataServices;
using CRM_Repository.Service;
using CRM_Repository.ServiceContract;
using System;
using System.Linq;
using System.Web.Mvc;
using CRM_Repository.Data;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace CRM.Controllers
{
    public class LoginController : Controller
    {
        private IUser_Repository _IUser_Repository;
        private IRolePermission_Repository _IRolePermission_Repository;
        EncryptDecrypt _Encdcrpt = new EncryptDecrypt();
        // GET: Login
        public ActionResult Index()
        {
           // string pass = _Encdcrpt.Encypt("gurjari@123$").ToString();
            return View();
        }
        public LoginController()
        {
            this._IUser_Repository = new User_Repository(new CRM_Repository.Data.elaunch_crmEntities());
            this._IRolePermission_Repository = new RolePermission_Repository(new CRM_Repository.Data.elaunch_crmEntities());
        }
        [HttpPost]
        public ActionResult Index(string username, string Pwd)
        {
            try
            {
                string pass = _Encdcrpt.Encypt(Pwd).ToString();
                var log = _IUser_Repository.CheckLogin(username, pass).ToList();
                if (log.Count > 0)
                {
                    var data = Request.Browser.Browser;
                    string IPAddress = Request.UserHostAddress;
                    //IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    //IPAddress ipadd = host
                    //    .AddressList
                    //    .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                    //var dt= Request.UserAgent;
                    LoginHistory loghis = new LoginHistory();
                    loghis.UserId = log[0].UserId;
                    loghis.IP = IPAddress.ToString();
                    loghis.Browser = data.ToString();
                    loghis.LoginTime = DateTime.Now;
                    if (Request.Browser.IsMobileDevice)
                    {
                        loghis.DeviceType = 1;
                    }
                    else
                    {
                        loghis.DeviceType = 2;
                    }
                    _IUser_Repository.AddLoginHistory(loghis);

                    System.Web.HttpContext.Current.Session["UserId"] = log[0].UserId;
                    System.Web.HttpContext.Current.Session["UserName"] = log[0].Name + " " + log[0].Surname;
                    System.Web.HttpContext.Current.Session["UserImg"] = log[0].Photo;
                    System.Web.HttpContext.Current.Session["UserRollType"] = log[0].RoleId;
                    System.Web.HttpContext.Current.Session["UserDeptId"] = log[0].DepartmentId;
                   // DataTable dtb = new DataTable();
                   // dtb = _IRolePermission_Repository.GetPermissionRecord();
                  //  HttpRuntime.Cache.Insert("Data", dtb, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                    //Cache.Insert("Data", dtb,null,Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                    
                    //return RedirectToAction("Index", "Dashboard", new { area = "Master" });
                    return RedirectToAction("Dashboard", "Dashboard", new { area = "Master" });
                }
                else
                {
                    ViewBag.error = "Username or Password is inccorect!!";
                }
                return View();
                //if (username == "admin" && Pwd == "admin")
                //{

                //}
                //else
                //{
                //    ViewBag.error = "Username or Password is inccorect!!";
                //}
                //return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult ChangePassword()
        {
            if (sessionUtils.HasUserLogin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass, string confirmpass)
        {
            try
            {
                if (sessionUtils.HasUserLogin())
                {
                    if (newpass.Trim() == confirmpass.Trim())
                    {
                        int uid = sessionUtils.UserId;
                        string oldpassword = _Encdcrpt.Encypt(oldpass);
                        var userdata = _IUser_Repository.CheckPasswordById(uid, oldpassword).ToList();
                        if (userdata.Count > 0)
                        {
                            string newpassword = _Encdcrpt.Encypt(newpass);
                            UserMaster objuser = _IUser_Repository.GetUserById(uid);
                            objuser.Password = newpassword;
                            _IUser_Repository.UpdateUser(objuser);
                            ViewBag.error = "Password Change Successfully";
                        }
                        else
                        {
                            ViewBag.error = "Invalid Old Password";
                        }
                    }
                    else
                    {
                        ViewBag.error = "Do Not Match New Password and Confirm Password";
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Logout()
        {
            if (sessionUtils.HasUserLogin())
            {
                System.Web.HttpContext.Current.Session["UserId"] = null;
                System.Web.HttpContext.Current.Session["UserName"] = null;
                System.Web.HttpContext.Current.Session["UserImg"] = null;
                System.Web.HttpContext.Current.Session["UserRollType"] = null;
                Session.Clear();
                Session.Abandon();

                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogInReport()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AttendanceReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Attendance(string time, string reason)
        {
            try
            {
                int id = int.Parse(Session["UserId"].ToString());
                AttendanceMaster atnd = _IUser_Repository.GetAttendancebyUserid(id, DateTime.Now);
                string IPAddress = Request.UserHostAddress;
                AttendanceMaster objAttendance;
                string mode = "";
                string msg = "";
                bool error = false;
                if (atnd == null)
                {
                    objAttendance = new AttendanceMaster();
                    objAttendance.UserId = id;
                    objAttendance.OnDate = DateTime.Now;
                    objAttendance.IsActive = true;
                    mode = "ADD";
                    if (objAttendance.WorkStartTime == null && time == "worktimeend")
                    {
                        msg = "Work is Not Started for today";
                        return Json(new { ok = false, msgerror = false, message = msg });
                    }
                    else if (objAttendance.LunchStartTime == null && time == "lunchtimeend")
                    {
                        msg = "Lunch is Not Started for today";
                        return Json(new { ok = false, msgerror = false, message = msg });
                    }
                    else if (objAttendance.OutSideStartTime == null && time == "outsidetimeend")
                    {
                        msg = "OutSide is Not Started for today";
                        return Json(new { ok = false, msgerror = false, message = msg });
                    }
                }
                else
                {
                    objAttendance = atnd;
                    mode = "EDIT";
                    if (objAttendance.WorkStartTime == null && time == "worktimeend")
                    {
                        msg = "Work is Not Started for today";
                        return Json(new { ok = false, msgerror = false, message = msg });
                    }
                    else if (objAttendance.LunchStartTime == null && time == "lunchtimeend")
                    {
                        msg = "Lunch is Not Started for today";
                        return Json(new { ok = false, msgerror = false, message = msg });
                    }
                    else if (objAttendance.OutSideStartTime == null && time == "outsidetimeend")
                    {
                        msg = "OutSide is Not Started for today";
                        return Json(new { ok = false, msgerror = false, message = msg });
                    }
                }

                if (time == "worktime")
                {
                    if (objAttendance.WorkStartTime == null)
                    {
                        objAttendance.WorkStartTime = DateTime.Now.TimeOfDay;
                        objAttendance.WorkStartIP = IPAddress;
                        msg = "Work is Started.";
                    }
                    else
                    {
                        msg = "Work is already Started for today";
                        error = true;
                    }

                }
                else if (time == "worktimeend")
                {
                    if (objAttendance.WorkEndTime == null)
                    {

                        if (objAttendance.LunchStartTime == null && objAttendance.LunchEndTime == null)
                        {
                            objAttendance.WorkEndTime = DateTime.Now.TimeOfDay;
                            objAttendance.WorkEndIP = IPAddress;
                            msg = "Work is Ended";
                        }
                        else if (objAttendance.LunchStartTime != null && objAttendance.LunchEndTime != null)
                        {
                            objAttendance.WorkEndTime = DateTime.Now.TimeOfDay;
                            objAttendance.WorkEndIP = IPAddress;
                            msg = "Work is Ended";

                        }
                        else
                        {
                            msg = "Lunch is Not Ended";
                            error = true;
                        }
                    }
                    else
                    {
                        msg = "Work is already ended for today.";
                        error = true;
                    }
                }
                else if (time == "lunchtime")
                {
                    if (objAttendance.WorkStartTime == null)
                    {
                        msg = "Work is Not Started for today.";
                        error = true;
                    }
                    else
                    {
                        if (objAttendance.LunchStartTime == null)
                        {
                            objAttendance.LunchStartTime = DateTime.Now.TimeOfDay;
                            objAttendance.LunchStartIP = IPAddress;
                            msg = "Lunch is started.";
                        }
                        else
                        {
                            msg = "Lunch is already started for today.";
                            error = true;
                        }
                    }
                }
                else if (time == "lunchtimeend")
                {
                    if (objAttendance.LunchEndTime == null)
                    {
                        objAttendance.LunchEndTime = DateTime.Now.TimeOfDay;
                        objAttendance.LunchEndIP = IPAddress;
                        msg = "Lunch is ended.";
                    }
                    else
                    {
                        msg = "Lunch is already ended for today.";
                        error = true;
                    }
                }
                else if (time == "outsideime")
                {
                    if (!string.IsNullOrEmpty(reason))
                    {
                        if (objAttendance.OutSideStartTime == null)
                        {
                            objAttendance.OutSideStartTime = DateTime.Now.TimeOfDay;
                            objAttendance.OutSideStartIP = IPAddress;
                            objAttendance.OutSideReason = reason;
                            msg = "OutSide is started.";
                        }
                        else
                        {
                            msg = "OutSide is already started for today.";
                            error = true;
                        }
                    }
                    else
                    {
                        if (objAttendance.OutSideStartTime == null)
                        {
                            msg = "OutSide Checked Value";
                            return Json(new { ok = false, msgerror = true, message = msg }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "OutSide is already started for today.";
                            return Json(new { ok = false, msgerror = false, message = msg }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else if (time == "outsidetimeend")
                {
                    if (objAttendance.OutSideEndTime == null)
                    {
                        objAttendance.OutSideEndTime = DateTime.Now.TimeOfDay;
                        objAttendance.OutSideEndIP = IPAddress;
                        msg = "OutSide is ended.";
                    }
                    else
                    {
                        msg = "OutSide is already ended for today.";
                        error = true;
                    }
                }
                else
                {
                    return Json(new { ok = false, message = "Error." });
                }

                if (mode == "ADD")
                {
                    if (error == false)
                    {
                        _IUser_Repository.AddAttendance(objAttendance);
                        if (reason != "")
                        {
                            return Json(new { ok = true, msgerror = true, message = msg }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { ok = true, msgerror = true, message = msg });
                        }
                    }
                    else
                    {
                        if (reason != "")
                        {
                            return Json(new { ok = true, msgerror = false, message = msg }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { ok = false, msgerror = false, message = msg });
                        }
                    }
                }
                else
                {
                    if (error == false)
                    {
                        _IUser_Repository.UpdateAttendance(objAttendance);
                        if (reason != "")
                        {
                            return Json(new { ok = true, msgerror = true, message = msg }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { ok = true, msgerror = true, message = msg });
                        }
                    }
                    else
                    {
                        if (reason != "")
                        {
                            return Json(new { ok = true, msgerror = false, message = msg }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { ok = false, msgerror = false, message = msg });
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { ok = true });

        }
        public ActionResult Attendance1(string time)
        {
            try
            {
                int id = int.Parse(Session["UserId"].ToString());
                AttendanceMaster atnd = _IUser_Repository.GetAttendancebyUserid(id, DateTime.Now);
                string IPAddress = Request.UserHostAddress;
                AttendanceMaster objAttendance;
                string mode = "";
                string msg = "";
                bool isPopup = false;
                if (atnd == null)
                {
                    objAttendance = new AttendanceMaster();
                    objAttendance.UserId = id;
                    objAttendance.OnDate = DateTime.Now;
                    objAttendance.IsActive = true;
                    mode = "ADD";
                    if (objAttendance.WorkStartTime == null && time == "worktimeend")
                    {
                        msg = "Work is Not Started for today";
                        return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }
                    else if (objAttendance.LunchStartTime == null && time == "lunchtimeend")
                    {
                        msg = "Lunch is Not Started for today";
                        return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    objAttendance = atnd;
                    mode = "EDIT";
                    if (objAttendance.WorkStartTime == null && time == "worktimeend")
                    {
                        msg = "Work is Not Started for today";
                        return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }
                    else if (objAttendance.LunchStartTime == null && time == "lunchtimeend")
                    {
                        msg = "Lunch is Not Started for today";
                        return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (time == "worktime")
                {
                    if (objAttendance.WorkStartTime == null)
                    {
                        objAttendance.WorkStartTime = DateTime.Now.TimeOfDay;
                        objAttendance.WorkStartIP = IPAddress;
                        msg = "Work is Started.";
                        isPopup = true;
                    }
                    else
                        msg = "Work is already Started for today";
                }
                else if (time == "worktimeend")
                {
                    if (objAttendance.WorkEndTime == null)
                    {
                        objAttendance.WorkEndTime = DateTime.Now.TimeOfDay;
                        objAttendance.WorkEndIP = IPAddress;
                        msg = "Work is Ended";
                        isPopup = true;
                    }
                    else
                        msg = "Work is already ended for today.";
                }
                else if (time == "lunchtime")
                {
                    if (objAttendance.LunchStartTime == null)
                    {
                        objAttendance.LunchStartTime = DateTime.Now.TimeOfDay;
                        objAttendance.LunchStartIP = IPAddress;
                        msg = "Lunch is started.";
                        isPopup = true;
                    }
                    else
                        msg = "Lunch is already started for today.";
                }
                else if (time == "lunchtimeend")
                {
                    if (objAttendance.LunchEndTime == null)
                    {
                        objAttendance.LunchEndTime = DateTime.Now.TimeOfDay;
                        objAttendance.LunchEndIP = IPAddress;
                        msg = "Lunch is ended.";
                        isPopup = true;
                    }
                    else
                        msg = "Lunch is already ended for today.";
                }
                else
                {
                    return Json(new { ok = false, message = "Error.", WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                }

                if (mode == "ADD")
                {
                    //_IUser_Repository.AddAttendance(objAttendance);
                    return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //_IUser_Repository.UpdateAttendance(objAttendance);
                    return Json(new { ok = true, message = msg, WorkType = time, isPopup = isPopup }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { ok = true, message = "", WorkType = time, isPopup = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult WorkTaskReport()
        {
            return View();
        }
    }
}