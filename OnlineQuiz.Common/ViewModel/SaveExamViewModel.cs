namespace OnlineQuiz.Common.ViewModel
{
    public class SaveExamViewModel
    {
        public string IDExaminee { get; set; }
        
        public string ExamResultID { get; set; }

        public int RemainingTime { get; set; }

        public string Content { get; set; }

        public bool Status { get; set; } = false;
    }
}
