namespace OnlineQuiz.Common.ViewModel
{
    public class ResponseBase
    {
        public bool Status { get; set; } = false;

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public object Model { get; set; }
    }
}
