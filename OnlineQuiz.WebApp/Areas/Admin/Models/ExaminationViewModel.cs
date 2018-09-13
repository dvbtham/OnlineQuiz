using System.Collections.Generic;

namespace OnlineQuiz.WebApp.Areas.Admin.Models
{
    public class ExaminationViewModel
    {
        public int ID { get; set; }

        public int? ExamPeriodID { get; set; }
        public ExamPeriodViewModel ExamPeriod { get; set; }

        public string Name
        {
            get
            {
                return ExamPeriod.ExaminationFormattedDate;
            }
        }

        public int? Duration { get; set; }

        public int? QuestionNumber { get; set; }

        public int? TestNumber { get; set; }

        public int? ExellentNumber { get; set; }

        public int? VeryGoodNumber { get; set; }

        public int? GoodNumber { get; set; }

        public int? AverageNumber { get; set; }

        public ICollection<ExaminationModuleViewModel> ExaminationModules { get; set; } = new List<ExaminationModuleViewModel>();

        public ICollection<ExaminationQuestionViewModel> ExaminationQuestions { get; set; } = new List<ExaminationQuestionViewModel>();
    }
}