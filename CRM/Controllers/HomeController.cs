using CRM.Models.Grid;
using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["UserId"] = 1;
            string a = "2016-09-28T18:30:00.000Z";
            DateTime b = Convert.ToDateTime(a);
            string c = b.ToString("MMM dd yyyy");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public string getData(GridReqData objGrid)
        {
            // objGrid.Mode = "UserMain";
            GridData obj = new GridData(objGrid);
            return obj.JsonData;
        }

        [HttpGet]
        public JsonResult GetAutoCompleteData(string keyword, string Mode, string RelatedValue)
        {
            Models.AutoComplete obj = new Models.AutoComplete(Mode, keyword, RelatedValue);
            return Json(obj.GetData(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckAutoComplete(string keyword, string Mode, string RelatedValue)
        {
            if (keyword.Trim() != "")
            {
                Models.AutoComplete obj = new Models.AutoComplete(Mode, keyword, RelatedValue, false, "CHECK");
                return Json(obj.GetData(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult getCountryData()
        {
            CRM_Repository.Service.Country_Repository obj = new CRM_Repository.Service.Country_Repository(new elaunch_crmEntities());
            List<CountryMaster> lst = obj.GetAllCountry().ToList();
            return Json(lst);
        }
       
    }
}