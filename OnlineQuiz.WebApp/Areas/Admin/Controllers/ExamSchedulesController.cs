using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Repositories;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Areas.Admin.Controllers
{
    public class ExamSchedulesController : Controller
    {
        private readonly IExamPeriodRepository examPeriodRepository;
        private readonly IExaminationRoomRepository examinationRoomRepository;
        private readonly IStartEndTimeRepository startEndTimeRepository;
        private readonly ITechSkillRepository techSkillRepository;
        private readonly IExamScheduleRepository examScheduleRepository;

        public ExamSchedulesController(IExamPeriodRepository examPeriodRepository,
            IExaminationRoomRepository examinationRoomRepository,
            IStartEndTimeRepository startEndTimeRepository,
            ITechSkillRepository techSkillRepository,
            IExamScheduleRepository examScheduleRepository)
        {
            this.examPeriodRepository = examPeriodRepository;
            this.examinationRoomRepository = examinationRoomRepository;
            this.startEndTimeRepository = startEndTimeRepository;
            this.techSkillRepository = techSkillRepository;
            this.examScheduleRepository = examScheduleRepository;
        }

        public ActionResult Index()
        {
            var vm = new ExamScheduleViewModel();
            ViewBag.ExamPeriods = new SelectList(examPeriodRepository.GetKeyValueList(), "Key", "Value");
            ViewBag.Rooms = new SelectList(examinationRoomRepository.GetKeyValueList(), "Key", "Value");
            ViewBag.StartEndTimes = new SelectList(startEndTimeRepository.GetKeyValueList(), "Key", "Value");
            ViewBag.TechSkills = new SelectList(techSkillRepository.GetKeyValueList(), "Key", "Value");

            return View(vm);
        }

        [HttpPost]
        public ActionResult SaveSchedule([System.Web.Http.FromBody]SaveScheduleViewModel data)
        {
            try
            {
                var responseBase = examScheduleRepository.SaveExamSchedule(viewModel: data);

                if (responseBase.Status)
                {
                    return Json(new
                    {
                        data = responseBase,
                        message = responseBase.Message
                    });
                }

                return Json(new
                {
                    stackTrack = responseBase.StackTrace
                });
            }
            catch (System.Exception e)
            {
                return Json(new
                {
                    data = e.Message
                });
            }

        }

        [HttpPost]
        public ActionResult GetExamClassName(string examPeriodId, string techId, int quantity, string techName, string moduleId)
        {
            return Json(new
            {
                data = examScheduleRepository.GenerateClass(examPeriodId, techId, quantity, techName, moduleId)
            });
        }
    }
}