using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Transaction.Controllers
{
    public class TodayWorkController : Controller
    {
        // GET: Transaction/TodayWork
        public ActionResult Index()
        {
            return View();
        }
    }
}