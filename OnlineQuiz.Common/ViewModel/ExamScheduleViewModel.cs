using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExamScheduleViewModel
    {
        public SaveExamScheduleViewModel SaveExamSchedule { get; set; }
        public ViewExamScheduleViewModel ViewExamSchedule { get; set; }
    }
}
