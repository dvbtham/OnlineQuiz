namespace OnlineQuiz.Common.ViewModel
{
    public class SaveExamViewModel
    {
        public string IDExaminee { get; set; }

        public byte? Page { get; set; }

        public string ExamResultID { get; set; }

        public int RemainingTime { get; set; }

        public string Content { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
