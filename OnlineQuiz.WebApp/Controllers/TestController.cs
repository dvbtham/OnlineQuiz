using Newtonsoft.Json;
using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Infrastructure;
using OnlineQuiz.Model.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Controllers
{
    public class TestController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountRepository accountRepository;
        private readonly IExaminationRepository examinationRepository;
        private readonly IExamResultRepository examResultRepository;

        public TestController(IUnitOfWork unitOfWork,
            IAccountRepository accountRepository,
            IExaminationRepository examinationRepository,
            IExamResultRepository examResultRepository
            )
        {
            this.unitOfWork = unitOfWork;
            this.accountRepository = accountRepository;
            this.examinationRepository = examinationRepository;
            this.examResultRepository = examResultRepository;
        }

        public ActionResult Index(string id)
        {
            var session = (ExamineeViewModel)Session["User"];
            if (session.IDExaminee != id)
                return RedirectToAction("Index", "Login");

            var examVm = GetExaminationViewModel(id);

            if (examVm.ExamResult.Status)
            {
                return RedirectToAction("ExamResult", new { examineeId = id });
            }

            return View(examVm);
        }

        public ActionResult ExamResult(string examineeId)
        {
            var session = (ExamineeViewModel)Session["User"];
            if (session.IDExaminee != examineeId)
                return RedirectToAction("Index", "Login");

            var examResult = examResultRepository.GetExamResult(examineeId, 1);

            return View(examResult);
        }

        [HttpPost]
        public ActionResult Save([System.Web.Http.FromBody] SaveExamViewModel data)
        {
            examResultRepository.CompleteTest(data.ExamResultID, isComplete: data.Status);

            examResultRepository.UpdateDuration(data.ExamResultID, data.RemainingTime);

            UpdateDetail(data);

            if (data.Status)
                return Json(new
                {
                    url = "/Test/ExamResult?examineeId=" + data.IDExaminee
                });

            return Json(new
            {
                model = data
            });

        }

        private void UpdateDetail(SaveExamViewModel data)
        {
            try
            {
                var questions = JsonConvert.DeserializeObject<string[]>(data.Content);

                foreach (var q in questions)
                {
                    var answer = q.Substring(0, 1);
                    var qid = q.Substring(2);

                    examResultRepository.UpdateDetail(data.ExamResultID, qid, new KeyValuePair { Key = answer });
                }

                unitOfWork.Commit();
            }
            catch (Exception)
            {
            }
        }
                

        [NonAction]
        private ExaminationViewModel GetExaminationViewModel(string id)
        {
            var examResult = examResultRepository.Get(id, 1);

            var questionList = examinationRepository.GetExaminationQuestions(examResult).ToList();

            var examVm = new ExaminationViewModel
            {
                ExamResult = examResult,
                ExaminationQuestions = questionList.OrderBy(x => x.QuestionID),
                Examinee = accountRepository.GetAttendanceInfo(id)
            };
            return examVm;
        }
    }
}