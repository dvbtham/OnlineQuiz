namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BasicExamResultDetail")]
    public partial class BasicExamResultDetail
    {
        public Guid ID { get; set; }

        public Guid? BasicExamResultID { get; set; }

        public Guid? QuesionID { get; set; }

        [StringLength(5)]
        public string Answer { get; set; }

        [StringLength(250)]
        public string ResultAswer { get; set; }

        public virtual BasicExamResult BasicExamResult { get; set; }

        public virtual Question Question { get; set; }
    }
}
