using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Common.ViewModel
{
    public class RegisterViewModel
    {
        public ExamineeViewModel Examinee { get; set; } = new ExamineeViewModel();

        public ExamPeriodViewModel ExamPeriod { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        [Display(Name = "Ngày đăng ký")]
        [DataType(DataType.Date, ErrorMessage = "Vui lòng nhập đúng định dạng ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
                        
        public string IdentityCard { get; set; }

        public KeyValuePair TechSkill { get; set; }

        public KeyValuePair Module { get; set; }

    }
}
