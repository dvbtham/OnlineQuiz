using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.WebApp.Areas.Admin.Models
{
    public class QuestionViewModel
    {
        public Guid ID { get; set; }
        public Guid? QuestionModuleID { get; set; }
        public Guid? QuestionClassificationID { get; set; }
        public string QuestionContent { get; set; }
        [StringLength(250)]
        public string AAnswer { get; set; }
        [StringLength(250)]
        public string BAnswer { get; set; }
        [StringLength(250)]
        public string CAnswer { get; set; }
        [StringLength(250)]
        public string DAnswer { get; set; }
        [StringLength(5)]
        public string Answer { get; set; }
        [StringLength(250)]
        public string ResultAnswer { get; set; }
    }
}