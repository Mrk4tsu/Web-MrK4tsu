using System.Web.Mvc;

namespace WebMrKatsu.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin Home",
                "dasboard",
                new {action = "Index", controller = "Home" }
                );

            context.MapRoute(
                "Admin Login",
                "login",
                new { action = "Index", controller = "Authenticate" }
                );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}