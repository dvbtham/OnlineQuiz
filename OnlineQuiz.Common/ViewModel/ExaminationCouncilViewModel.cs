using System;
using System.Web.Mvc;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExaminationCouncilViewModel
    {
        public string ExaminationCouncilName { get; set; }

        public SelectList Modules { get; set; }

        public SelectList QuestionModules { get; set; }

        public DateTime ExaminationDate { get; set; }

        public string ExaminationTime { get; set; }

    }
}
