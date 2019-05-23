using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_Repository.DTOModel;

namespace CRM.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            if (sessionUtils.HasUserLogin())
            {
                ReportPara objPara = new ReportPara();
                return View(objPara);
            }
            else
                return RedirectToAction("Index", "Login");
        }

    }
}