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