using System.Collections.Generic;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExaminationViewModel
    {
        public byte Page { get; set; }
        public int TotalItems { get; set; }
        public byte TotalPages { get; set; }
        public AttendanceViewModel Examinee { get; set; }
        public IEnumerable<ExaminationQuestionViewModel> ExaminationQuestions { get; set; }
    }
}
