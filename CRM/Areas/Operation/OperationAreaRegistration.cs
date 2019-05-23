using System.Web.Mvc;

namespace CRM.Areas.Operation
{
    public class OperationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Operation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Operation_default",
                "Operation/{controller}/{action}/{id}/{temp}",
                new { action = "Index", id = UrlParameter.Optional, temp = UrlParameter.Optional }
            );
        }
    }
}