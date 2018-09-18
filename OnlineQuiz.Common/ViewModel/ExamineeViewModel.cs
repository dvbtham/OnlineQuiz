using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExamineeViewModel
    {
        public Guid ID { get; set; }


        public string IDExaminee { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        [Display(Name ="Họ lót")]
        [StringLength(50, ErrorMessage = "{0} chỉ được nhập tối đa {1} ký tự.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        [Display(Name = "Tên")]
        [StringLength(50, ErrorMessage = "{0} chỉ được nhập tối đa {1} ký tự.")]
        public string LastName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Vui lòng nhập đúng định dạng ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Bạn chưa chọn {0}!")]
        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        [Display(Name = "Số chứng minh thư")]
        [StringLength(15, ErrorMessage = "{0} chỉ được nhập tối đa {1} ký tự.")]
        public string IdentityCard { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        public string Mobile { get; set; }
    }
}
