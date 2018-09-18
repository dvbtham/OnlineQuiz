namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ExamResultDetail")]
    public partial class ExamResultDetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid ExamResultID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid QuestionID { get; set; }

        [StringLength(1)]
        public string Answer { get; set; }

        public bool Status { get; set; }

        [StringLength(250)]
        public string ResultAnswer { get; set; }

        public virtual ExamResult ExamResult { get; set; }

        public virtual Question Question { get; set; }
    }
}
