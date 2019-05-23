using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRM.App_Start
{
    public class HasLoginSessionFilter : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["UserId"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var urlHelper = new UrlHelper(filterContext.RequestContext);
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Error = "NotAuthorized",
                            LogOnUrl = new RedirectResult(string.Format("/Login/Index"))
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new RedirectResult(string.Format("/Login/Index"));
                    return;
                }
            }
            //if (HttpContext.Current.Session["UserId"] == null )
            //{
            //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            //    redirectTargetDictionary.Add("action", "Index");
            //    redirectTargetDictionary.Add("controller", "Login");
            //    redirectTargetDictionary.Add("area", null);
            //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            //    return;
            //}
        }
    }
}