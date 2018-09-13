using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.WebApp.Areas.Admin.Models
{
    public class ExamPeriodViewModel
    {
        public int ID { get; set; }

        public DateTime? ExaminationDate { get; set; }

        public string ExaminationFormattedDate
        {
            get
            {
                return ExaminationDate.HasValue ? ExaminationDate.Value.ToString("dd/MM/yyyy") : "";
            }
        }

        [StringLength(100)]
        public string StartEndTime { get; set; }
    }
}