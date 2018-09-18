using Newtonsoft.Json;
using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Infrastructure;
using OnlineQuiz.Model.Repositories;
using System;
using System.IO;
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
            byte? page = 1;
            var session = (ExamineeViewModel)Session["User"];
            if (session.IDExaminee != id)
                return RedirectToAction("Index", "Login");

            var examVm = GetExaminationViewModel(id, page);
           
            return View(examVm);
        }

        [HttpPost]
        public ActionResult Save([System.Web.Http.FromBody] SaveExamViewModel data)
        {
            if (data.Page > 0)
            {
                examResultRepository
                .UpdateDuration(data.ExamResultID, data.RemainingTime);

                var examVm = GetExaminationViewModel(data.IDExaminee, data.Page);

                Update(data);

                return Json(new
                {
                    isRenderHtml = true,
                    data = ConvertViewToString("_Index", examVm)
                });
            }
            else
            {
                examResultRepository.CompleteTest(data.ExamResultID);
                Update(data);
                return Json(new
                {
                    isRenderHtml = false,
                    data = "Nộp bài hoàn tất"
                });
            }
        }

        private void Update(SaveExamViewModel data)
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

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        [NonAction]
        private ExaminationViewModel GetExaminationViewModel(string id, byte? page = 1)
        {
            var examResult = examResultRepository.Get(id, 1);

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var questionList = examinationRepository.GetExaminationQuestions(examResult).ToList();
            int totalCount = questionList.Count;

            var examVm = new ExaminationViewModel
            {
                ExamResult = examResult,
                Page = page.Value,
                TotalItems = totalCount,
                TotalPages = (byte)Math.Ceiling((double)totalCount / pageSize),
                ExaminationQuestions = questionList.OrderBy(x => x.QuestionID).Skip((page.Value - 1) * pageSize).Take(pageSize),
                Examinee = accountRepository.GetAttendanceInfo(id)
            };
            return examVm;
        }
    }
}