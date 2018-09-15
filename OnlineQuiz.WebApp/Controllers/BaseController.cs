using OnlineQuiz.Common.ViewModel;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineQuiz.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (ExamineeViewModel)Session["User"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}