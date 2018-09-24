using OnlineQuiz.Model.Repositories;
using System.Web.Mvc;

namespace OnlineQuiz.WebApp.Areas.Admin.Controllers
{
    public class ExaminationRoomsController : Controller
    {
        private readonly IExaminationRoomRepository examinationRoomRepository;

        public ExaminationRoomsController(IExaminationRoomRepository examinationRoomRepository)
        {
            this.examinationRoomRepository = examinationRoomRepository;
        }

        [HttpPost]
        public ActionResult GetKeyValue(string id)
        {
            return Json(new
            {
                data = examinationRoomRepository.GetById(id)
            });
        }
    }
}