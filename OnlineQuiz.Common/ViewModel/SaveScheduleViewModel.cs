using System;

namespace OnlineQuiz.Common.ViewModel
{
    public class SaveScheduleViewModel
    {
        public Guid ExamPeriodID { get; set; }
        public DateTime ExaminationDate { get; set; }
        public Guid StartEndTimeID { get; set; }
        public Guid ExaminationRoomID { get; set; }
        public Guid InformationTechnologyID { get; set; }
        public int ExamineeQuantityOfRoom { get; set; }
        public string Remark { get; set; }
        public Guid QuestionModuleID { get; set; }

        /// <summary>
        /// Để xác định là module cơ bản hay nâng cao
        /// </summary>
        public string TechName { get; set; }
    }
}
