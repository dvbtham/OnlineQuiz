using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExamScheduleRepository
    {
        ResponseBase SaveExamSchedule(SaveScheduleViewModel viewModel);
        IEnumerable<KeyValuePair> GenerateClass(string examPeriodId, string techId, int quantity, string techName, string moduleId);
    }

    public class ExamScheduleRepository : RepositoryBase<ExamScheduleBasic>, IExamScheduleRepository
    {
        public ExamScheduleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<KeyValuePair> GenerateClass(string examPeriodId, string techId, int quantity, string techName, string moduleId)
        {
            if (techName.Contains("cơ bản"))
            {
                var pars = new SqlParameter[]
                {
                    new SqlParameter("@ExamPeriodID", examPeriodId),
                    new SqlParameter("@InformationTechnologyID", techId),
                    new SqlParameter("@ExamineeQuantityOfRoom", quantity),
                };

                return DbContext.Database
                    .SqlQuery<KeyValuePair>("spExaminationClass @ExamPeriodID, @InformationTechnologyID, @ExamineeQuantityOfRoom", pars).ToList();
            }
            else
            {
                var pars = new SqlParameter[]
               {
                    new SqlParameter("@ExamPeriodID", examPeriodId),
                    new SqlParameter("@InformationTechnologyID", techId),
                    new SqlParameter("@QuestionModuleID", moduleId),
                    new SqlParameter("@ExamineeQuantityOfRoom", quantity),
               };

                return DbContext.Database
                    .SqlQuery<KeyValuePair>("spExaminationClassAdvanced @ExamPeriodID, @InformationTechnologyID, @QuestionModuleID, @ExamineeQuantityOfRoom", pars).ToList();
            }

        }

        public ResponseBase SaveExamSchedule(SaveScheduleViewModel viewModel)
        {
            try
            {
                if (viewModel.TechName.Contains("cơ bản"))
                {
                    var examBasicSchedule = new ExamScheduleBasic
                    {
                        ID = Guid.NewGuid(),
                        ExaminationDate = viewModel.ExaminationDate,
                        ExaminationRoomID = viewModel.ExaminationRoomID,
                        ExamPeriodID = viewModel.ExamPeriodID,
                        StartEndTimeID = viewModel.StartEndTimeID,
                    };

                    var pars = new SqlParameter[]
                    {
                        new SqlParameter("@ExamPeriodID", viewModel.ExamPeriodID),
                        new SqlParameter("@ExaminationDate", viewModel.ExaminationDate),
                        new SqlParameter("@StartEndTimeID", viewModel.StartEndTimeID),
                        new SqlParameter("@ExaminationRoomID", viewModel.ExaminationRoomID),
                        new SqlParameter("@InformationTechnologyID", viewModel.InformationTechnologyID),
                        new SqlParameter("@ExamineeQuantityOfRoom", viewModel.ExamineeQuantityOfRoom),
                        new SqlParameter("@Remark", viewModel.Remark),
                    };

                    var saved = DbContext.Database
                        .SqlQuery<ResponseBase>("spExamScheduleBasic @ExamPeriodID, @ExaminationDate, @StartEndTimeID, @ExaminationRoomID, @InformationTechnologyID, @ExamineeQuantityOfRoom, @Remark", pars).FirstOrDefault();
                }
                else
                {
                    var pars = new SqlParameter[]
                     {
                        new SqlParameter("@ExamPeriodID", viewModel.ExamPeriodID),
                        new SqlParameter("@ExaminationDate", viewModel.ExaminationDate),
                        new SqlParameter("@StartEndTimeID", viewModel.StartEndTimeID),
                        new SqlParameter("@ExaminationRoomID", viewModel.ExaminationRoomID),
                        new SqlParameter("@InformationTechnologyID", viewModel.InformationTechnologyID),
                        new SqlParameter("@QuestionModuleID", viewModel.QuestionModuleID),
                        new SqlParameter("@ExamineeQuantityOfRoom", viewModel.ExamineeQuantityOfRoom),
                        new SqlParameter("@Remark", viewModel.Remark),
                     };

                    var saved = DbContext.Database
                        .SqlQuery<ResponseBase>("spExamScheduleAdvanced @ExamPeriodID, @ExaminationDate, @StartEndTimeID, @ExaminationRoomID, @InformationTechnologyID, @QuestionModuleID, @ExamineeQuantityOfRoom, @Remark", pars).FirstOrDefault();
                }
                return new ResponseBase { Status = true, Message = "Tạo lịch thành công" };
            }
            catch (System.Exception e)
            {
                return new ResponseBase { Status = false, StackTrace = e.StackTrace, Message = e.Message };
            }
        }
    }
}
