namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineeExamScheduleBasic")]
    public partial class ExamineeExamScheduleBasic
    {
        public Guid ID { get; set; }

        public Guid? ExamScheduleBasicID { get; set; }

        public Guid? ExamineeID { get; set; }

        public virtual Examinee Examinee { get; set; }

        public virtual ExamScheduleBasic ExamScheduleBasic { get; set; }
    }
}
