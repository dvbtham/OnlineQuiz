using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Repositories;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Controllers
{
    public class ExamineeController : Controller
    {
        private readonly IAccountRepository accountRepository;

        public ExamineeController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string id)
        {
            var session = (ExamineeViewModel)Session["User"];
            if (session.IDExaminee != id)
                return RedirectToAction("Index", "Login");

            var examineeVm = accountRepository.GetAttendanceInfo(id);
           
            return View(examineeVm);
        }

        [HttpPost]
        public ActionResult EditExamineeInfo([System.Web.Http.FromBody]RequestEditViewModel data)
        {
            var result = accountRepository.Edit(data);
            if (result.Status)
            {
                return Json(new
                {
                    message = result.Message,
                    data = (RequestEditViewModel)result.Model
                });
            }
            else
            {
                return HttpNotFound(result.StackTrace);               
            }
        }
    }
}