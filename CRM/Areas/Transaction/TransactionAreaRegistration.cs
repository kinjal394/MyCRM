using System.Web.Mvc;

namespace CRM.Areas.Transaction
{
    public class TransactionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Transaction";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "Transaction_default",
            //    "Transaction/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
            context.MapRoute(
                "Transaction_default",
                "Transaction/{controller}/{action}/{id}/{temp}",
                new { action = "Index", id = UrlParameter.Optional, temp = UrlParameter.Optional }
            );
            
        }
    }
}