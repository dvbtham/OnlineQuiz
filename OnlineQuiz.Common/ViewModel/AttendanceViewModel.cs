using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class AttendanceViewModel
    {
        public Guid ID { get; set; }
        public string IDExaminee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string IdentityCard { get; set; }

        public Guid QuestionModuleID { get; set; }
        public string InformationTechnologySkillName { get; set; }
        public string QuestionModuleName { get; set; }
        public string Remark { get; set; }
        public DateTime ExaminationDate { get; set; }
    }
}
