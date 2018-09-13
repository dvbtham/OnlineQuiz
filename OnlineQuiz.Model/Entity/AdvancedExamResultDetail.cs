namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdvancedExamResultDetail")]
    public partial class AdvancedExamResultDetail
    {
        public Guid ID { get; set; }

        public Guid? AdvancedExamResultID { get; set; }

        public Guid? QuestionID { get; set; }

        [StringLength(5)]
        public string Answer { get; set; }

        [StringLength(250)]
        public string ResultAswer { get; set; }

        public virtual AdvancedExamResult AdvancedExamResult { get; set; }

        public virtual Question Question { get; set; }
    }
}
