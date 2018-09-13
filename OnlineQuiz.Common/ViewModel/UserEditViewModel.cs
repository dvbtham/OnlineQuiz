using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class UserEditViewModel 
    {
        public string IDExaminee { get; set; }

        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public bool Gender { get; set; }

        public string CMND { get; set; }
    }
}
