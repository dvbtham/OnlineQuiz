using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExamPeriodViewModel
    {
        public Guid ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }
    }
}
