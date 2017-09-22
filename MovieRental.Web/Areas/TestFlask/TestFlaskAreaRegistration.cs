using System.Web.Mvc;

namespace MovieRental.Web.Areas.TestFlask
{
    public class TestFlaskAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TestFlask";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TestFlask_Default",
                "TestFlask/{action}/{id}",
                new { controller="TestFlask", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}