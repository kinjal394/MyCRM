using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Master.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Master/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}