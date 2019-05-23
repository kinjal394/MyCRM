using CRM.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Product.Controllers
{
    [HasLoginSessionFilter]
    public class ProductFormController : Controller
    {
        // GET: Product/ProductForm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProductForm(int id = 0, int temp = 0)
        {
            ViewBag.id = id;
            ViewBag.isdisable = temp;
            return View();
        }
        public ActionResult ProductFormShare()
        {
            //ViewBag.id = id;
            return View();
        }
        public FileResult PrintKeyword(string Key)
        {
            Response.Clear();
            Response.AddHeader("Content-disposition", string.Format("attachment; filename=\"{0}\";", "keyword.txt"));
            return File(System.Text.Encoding.ASCII.GetBytes(Key), "text/plain");
        }



    }
}