using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Common.ViewModel
{
    public class SaveExamScheduleViewModel
    {
        public Guid ID { get; set; }

        [Display(Name = "Module nâng cao")]
        public Guid? AdvancedModuleRegistrationID { get; set; }

        [Display(Name = "Đợt thi")]
        [Required(ErrorMessage = "Bạn chưa chọn {0}")]
        public Guid ExamPeriodID { get; set; }

        [Display(Name = "Ngày thi")]
        public DateTime ExaminationDate { get; set; } = DateTime.Now;

        [Display(Name = "Xuất thi")]
        [Required(ErrorMessage = "Bạn chưa chọn {0}")]
        public Guid StartEndTimeID { get; set; }

        [Display(Name = "Phòng thi")]
        [Required(ErrorMessage = "Bạn chưa chọn {0}")]
        public Guid ExaminationRoomID { get; set; }
    }
}
