using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DemoGrid()
        //{
        //    return View();
        //}


        public ActionResult DemoGrid()
        {
            return View();
        }
        public ActionResult DemoAutoComplete()
        {
            return View();
        }
        public ActionResult ModalDemo()
        {
            return PartialView();
        }

        public ActionResult DemoAutoComplete2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DemoPopup(string Mode, string RelatedValue, string cntrlId)
        {
            ViewBag.Mode = Mode;
            Models.AutoComplete obj = new Models.AutoComplete(Mode, "", RelatedValue, true);
            ViewBag.Columns = JsonConvert.SerializeObject(obj.PopupColumns);
            ViewBag.Display = obj.DisplayColumnName;
            ViewBag.Value = obj.ValueColumnName;
            ViewBag.RelatedValue = GetProperColumnName(obj.WhereCalue);
            ViewBag.cntrlId = cntrlId;
            ViewBag.Title = obj.PopupTitle;
            return View();
        }

        public string GetProperColumnName(string column)
        {
            string[] ignoreList = new string[2];
            ignoreList[0] = "like";
            ignoreList[1] = "%";
            if (string.IsNullOrEmpty(column))
            {
                return "";
            }
            string[] results = column.Split(' ');
            string realColName = "";
            foreach (string val in results)
            {
                if (ignoreList.Contains(val))
                    realColName += val + " ";
                else
                {
                    string[] vals = val.Split('.');
                    if (vals.Length > 1)
                    {
                        if (val.Contains("("))
                        {
                            realColName += vals[0].Split('(')[0] + "(" + vals[1];
                        }
                        else
                            realColName += vals[1] + " ";
                    }
                    else
                        realColName += val + " ";
                }
            }
            return realColName;
        }
    }
}