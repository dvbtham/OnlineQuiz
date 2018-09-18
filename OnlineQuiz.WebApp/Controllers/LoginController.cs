using OnlineQuiz.Common.ViewModel;
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
        private readonly IExamineeRepository examineeRepository;
        private readonly ITechSkillRepository techSkillRepository;
        private readonly IExamPeriodRepository examPeriodRepository;
        private readonly IQuestionModuleRepository questionModuleRepository;

        public LoginController(INoteService noteService, 
            IAccountRepository accountRepository, 
            IExamineeRepository examineeRepository,
            ITechSkillRepository techSkillRepository,
            IExamPeriodRepository examPeriodRepository,
            IQuestionModuleRepository questionModuleRepository)
        {
            this.noteService = noteService;
            this.accountRepository = accountRepository;
            this.examineeRepository = examineeRepository;
            this.techSkillRepository = techSkillRepository;
            this.examPeriodRepository = examPeriodRepository;
            this.questionModuleRepository = questionModuleRepository;
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

                return RedirectToAction("Detail", "Examinee", new { id = loginResult.UserInfo.IDExaminee });
            }
            else
            {
                ModelState.AddModelError("invalid_account", "Tài khoản của bạn không tồn tại");
                return View(GetLoginViewModel());
            }

        }

        public ActionResult Register(string ic = null)
        {
            var vm = new RegisterViewModel();
            if (!string.IsNullOrEmpty(ic))
            {
                vm.Examinee = examineeRepository.FindByIdentityCard(ic);
            }

            ViewBag.ExamPeriods = new SelectList(examPeriodRepository.GetKeyValueList(), "Key", "Value");
            ViewBag.TechSkills = new SelectList(techSkillRepository.GetKeyValueList(), "Key", "Value");
            ViewBag.Modules = new SelectList(questionModuleRepository.GetKeyValueList(), "Key", "Value");
            return View(vm);
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