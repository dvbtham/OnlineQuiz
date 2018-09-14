using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Controllers
{
    public class TestController : BaseController
    {
        private readonly IAccountRepository accountRepository;
        private readonly IExaminationRepository examinationRepository;

        public TestController(IAccountRepository accountRepository, IExaminationRepository examinationRepository)
        {
            this.accountRepository = accountRepository;
            this.examinationRepository = examinationRepository;
        }

        public ActionResult Index(string id, byte? page = 1)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var questionList = examinationRepository.GetExaminationQuestions(id).ToList();
            int totalCount = questionList.Count;
            var examVm = new ExaminationViewModel
            {
                Page = page.Value,
                TotalItems = totalCount,
                TotalPages = (byte)Math.Ceiling((double)totalCount / 2),
                ExaminationQuestions = questionList.OrderBy(x => x.QuestionID).Skip((page.Value - 1) * pageSize).Take(pageSize),
                Examinee = accountRepository.GetAttendanceInfo(id)
            };
            return View(examVm);
        }
    }
}