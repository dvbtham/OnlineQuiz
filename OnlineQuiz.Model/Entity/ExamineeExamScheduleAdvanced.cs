namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineeExamScheduleAdvanced")]
    public partial class ExamineeExamScheduleAdvanced
    {
        public Guid ID { get; set; }

        public Guid? ExamScheduleAdvancedID { get; set; }

        public Guid? ExamineeID { get; set; }

        public virtual Examinee Examinee { get; set; }

        public virtual ExamScheduleAdvanced ExamScheduleAdvanced { get; set; }
    }
}
