using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExamineeViewModel
    {
        public string ID { get; set; }
        public string ExamineeID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string CMND { get; set; }
    }
}
