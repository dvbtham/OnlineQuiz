using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Controllers
{
    public class ExamineeController : BaseController
    {
        private readonly IAccountRepository accountRepository;

        public ExamineeController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public ActionResult Detail(string id)
        {
            var examineeVm = accountRepository.GetAttendanceInfo(id);
            ViewBag.Modules = examineeVm.ExaminationCouncil.Modules;
            ViewBag.QuestionModules = examineeVm.ExaminationCouncil.QuestionModules;
            return View(examineeVm);
        }

        public ActionResult BasicInfo(string id)
        {
            var examInfo = accountRepository.GetBasicExamInfo(id);
            return Json(new
            {
                data = examInfo
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdvanceInfo(string id, string title)
        {
            var examInfo = accountRepository.GetAdvcExamInfo(id, title);
            return Json(new
            {
                data = examInfo
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdvanceModules(string id)
        {
            var examInfo = accountRepository.GetAdvcModules(id);
            return Json(new
            {
                data = examInfo
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditExamineeInfo([System.Web.Http.FromBody]UserEditViewModel data)
        {
            var result = accountRepository.Edit(data);
            if (result.Status)
            {
                return Json(new
                {
                    message = result.Message,
                    data = (UserEditViewModel)result.Model
                });
            }
            else
            {
                return HttpNotFound(result.StackTrace);               
            }
        }
    }
}