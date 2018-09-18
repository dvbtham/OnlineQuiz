using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class InsertRegistrationViewModel
    {
        public string IdentityCard { get; set; }

        public Guid ExamPeriodId { get; set; }

        public Guid InformationTechnologySkillId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid QuestionModuleId { get; set; }
    }
}
