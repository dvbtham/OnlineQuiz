using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExamResultViewModel
    {
        public Guid ID { get; set; }

        public string IDExaminee { get; set; }

        public Guid ExaminationID { get; set; }

        public int ExamCode { get; set; }

        public int Duration { get; set; }

        public bool IsCompleted { get; set; }
    }
}
