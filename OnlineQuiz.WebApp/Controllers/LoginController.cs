using OnlineQuiz.Model.Repositories;
using OnlineQuiz.Service.Services;
using OnlineQuiz.WebApp.Models;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly INoteService noteService;
        private readonly IAccountRepository accountRepository;

        public LoginController(INoteService noteService, IAccountRepository accountRepository)
        {
            this.noteService = noteService;
            this.accountRepository = accountRepository;
        }

        public ActionResult Index()
        {
            var vm = new LoginViewModel
            {
                Notes = noteService.GetAll()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(GetLoginViewModel());
            var loginResult = accountRepository.CheckLogin(viewModel.ExamineeId, viewModel.Password);
            if (loginResult.Status)
            {
                Session["User"] = loginResult.UserInfo;

                return RedirectToAction("Detail", "Examinee", new { id = loginResult.UserInfo.Key });
            }
            else
            {
                ModelState.AddModelError("invalid_account", "Tài khoản của bạn không tồn tại");
                return View(GetLoginViewModel());
            }

        }

        [NonAction]
        public LoginViewModel GetLoginViewModel()
        {
            var vm = new LoginViewModel
            {
                Notes = noteService.GetAll()
            };
            return vm;
        }
    }
}