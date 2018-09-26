using System.Collections.Generic;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExaminationViewModel
    {
        public ExamResultViewModel ExamResult { get; set; }
        public AttendanceViewModel Examinee { get; set; }
        public IEnumerable<ExaminationQuestionViewModel> ExaminationQuestions { get; set; }
    }
}
