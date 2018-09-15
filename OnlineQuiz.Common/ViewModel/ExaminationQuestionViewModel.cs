using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExaminationQuestionViewModel
    {
        public Guid ID { get; set; }

        public Guid? ExaminationID { get; set; }

        public int? ExamCode { get; set; }

        public Guid? QuestionID { get; set; }

        public string QuestionContent { get; set; }

        public string AAnswer { get; set; }

        public string BAnswer { get; set; }

        public string CAnswer { get; set; }

        public string DAnswer { get; set; }

        public string Answer { get; set; }

        public string MyAnswer { get; set; }

        public string ResultAnswer { get; set; }

        public bool? Status { get; set; }
    }
}
