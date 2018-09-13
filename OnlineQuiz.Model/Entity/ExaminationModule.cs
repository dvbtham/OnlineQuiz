namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExaminationModule")]
    public partial class ExaminationModule
    {
        [Key]
        [Column(Order = 0)]
        public Guid ExaminationID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid QuestionModuleID { get; set; }

        public int? ExellentNumber { get; set; }

        public int? VeryGoodNumber { get; set; }

        public int? GoodNumber { get; set; }

        public int? AverageNumber { get; set; }

        public virtual Examination Examination { get; set; }

        public virtual QuestionModule QuestionModule { get; set; }
    }
}
