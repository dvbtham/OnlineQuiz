namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExaminationQuestion")]
    public partial class ExaminationQuestion
    {
        public Guid ID { get; set; }

        public Guid? ExaminationID { get; set; }

        public int? ExamCode { get; set; }

        public Guid? QuestionID { get; set; }

        [StringLength(450)]
        public string QuestionContent { get; set; }

        [StringLength(255)]
        public string AAnswer { get; set; }

        [StringLength(255)]
        public string BAnswer { get; set; }

        [StringLength(255)]
        public string CAnswer { get; set; }

        [StringLength(255)]
        public string DAnswer { get; set; }

        [StringLength(1)]
        public string Answer { get; set; }

        [StringLength(255)]
        public string ResultAnswer { get; set; }

        public bool? Status { get; set; }

        public virtual Examination Examination { get; set; }

        public virtual Question Question { get; set; }
    }
}
