using OnlineQuiz.Common.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập mã thí sinh")]
        [StringLength(50, ErrorMessage = "{0} chỉ nhập tối đa {1} ký tự")]
        [Display(Name = "Mã thí sinh")]
        public string ExamineeId { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        public IEnumerable<KeyValuePair> Notes { get; set; }
    }
}