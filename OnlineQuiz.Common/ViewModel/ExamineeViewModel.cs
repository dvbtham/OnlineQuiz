using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExamineeViewModel
    {
        public Guid ID { get; set; }
        public string IDExaminee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string IdentityCard { get; set; }
    }
}
