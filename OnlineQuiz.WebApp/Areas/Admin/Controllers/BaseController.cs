using OnlineQuiz.Common;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == AlertClass.Success.ToDescriptionString())
            {
                TempData["AlertType"] = "alert-success";
            }
            else
                if (type == AlertClass.Warning.ToDescriptionString())
            {
                TempData["AlertType"] = "alert-warning";
            }
            else
            if (type == AlertClass.Error.ToDescriptionString())
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}