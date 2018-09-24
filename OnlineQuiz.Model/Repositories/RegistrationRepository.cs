﻿using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IRegistrationRepository
    {
        ResponseBase InsertAdvancedModuleRegistration(InsertRegistrationViewModel vm);
        ResponseBase InsertBasicRegistration(InsertRegistrationViewModel vm);

        IEnumerable<RegistrationResultViewModel> GetBasicResult(string idCard, string examPeriodId);
        IEnumerable<RegistrationResultViewModel> GetAdvanceResult(string idCard, string examPeriodId);
    }

    public class RegistrationRepository : RepositoryBase<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public ResponseBase InsertAdvancedModuleRegistration(InsertRegistrationViewModel vm)
        {
            try
            {
                var examinee = DbContext.Examinees.FirstOrDefault(x => x.IdentityCard == vm.IdentityCard);

                var entity = GetSingleByCondition(x => x.ExamineeID.Value == examinee.ID && x.ExamPeriodID.Value == vm.ExamPeriodId && x.InformationTechnologySkillID.Value == vm.InformationTechnologySkillId);

                if (entity != null)
                {
                    return new ResponseBase()
                    {
                        Status = false,
                        Message = "Bạn đã đăng ký đợt thi này"
                    };
                }

                var registration = new Registration
                {
                    ID = Guid.NewGuid(),
                    ExamineeID = examinee.ID,
                    ExamPeriodID = vm.ExamPeriodId,
                    InformationTechnologySkillID = vm.InformationTechnologySkillId,
                    RegistrationDate = vm.RegistrationDate
                };

                Add(registration);
                DbContext.SaveChanges();

                var exmaineeVm = GetExamineeIDAdvn(registration.ID.ToString());
                var adcnReg = new AdvancedModuleRegistration()
                {
                    ID = Guid.NewGuid(),
                    IDExaminee = exmaineeVm.IDExaminee,
                    QuestionModuleID = vm.QuestionModuleId,
                    RegistrationID = registration.ID

                };

                DbContext.AdvancedModuleRegistrations.Add(adcnReg);


                return new ResponseBase()
                {
                    Status = true,
                    Message = "Đăng ký thành công"
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ExamineeViewModel GetExamineeID(string registrationID)
        {
            var para = new SqlParameter("@RegistrationId", registrationID);
            return DbContext.Database
                .SqlQuery<ExamineeViewModel>("spGenerateIDExaminee @RegistrationId", para).FirstOrDefault();
        }

        public ExamineeViewModel GetExamineeIDAdvn(string registrationID)
        {
            var para = new SqlParameter("@RegistrationId", registrationID);
            return DbContext.Database
                .SqlQuery<ExamineeViewModel>("spGenerateIDExamineeAdvn @RegistrationId", para).FirstOrDefault();
        }

        public ResponseBase InsertBasicRegistration(InsertRegistrationViewModel vm)
        {
            try
            {
                var examinee = DbContext.Examinees.FirstOrDefault(x => x.IdentityCard == vm.IdentityCard);

                var entity = GetSingleByCondition(x => x.ExamineeID.Value == examinee.ID && x.ExamPeriodID.Value == vm.ExamPeriodId && x.InformationTechnologySkillID.Value == vm.InformationTechnologySkillId);
                if (entity != null)
                {
                    return new ResponseBase()
                    {
                        Status = false,
                        Message = "Bạn đã đăng ký đợt thi này"
                    };
                }

                var registration = new Registration
                {
                    ID = Guid.NewGuid(),
                    ExamineeID = examinee.ID,
                    ExamPeriodID = vm.ExamPeriodId,
                    InformationTechnologySkillID = vm.InformationTechnologySkillId,
                    RegistrationDate = vm.RegistrationDate
                };

                Add(registration);
                DbContext.SaveChanges();

                var exmaineeVm = GetExamineeID(registration.ID.ToString());
                var idreg = new IDExamineeRegistration()
                {
                    IDExaminee = exmaineeVm.IDExaminee,
                    RegistrationID = registration.ID
                };

                DbContext.IDExamineeRegistrations.Add(idreg);

                return new ResponseBase()
                {
                    Status = true,
                    Message = "Đăng ký thành công"
                };

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<RegistrationResultViewModel> GetBasicResult(string idCard, string examPeriodId)
        {
            try
            {
                var pars = new SqlParameter[]
                {
                    new SqlParameter("@IdentityCard", idCard),
                    new SqlParameter("@ExamPeriodId", examPeriodId)
                };
                return DbContext.Database
                    .SqlQuery<RegistrationResultViewModel>("spGetRegistrationByIdentityCardExamPeriodId @IdentityCard, @ExamPeriodId", pars).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<RegistrationResultViewModel> GetAdvanceResult(string idCard, string examPeriodId)
        {
            try
            {
                var pars = new SqlParameter[]
               {
                    new SqlParameter("@IdentityCard", idCard),
                    new SqlParameter("@ExamPeriodId", examPeriodId)
               };
                return DbContext.Database
                    .SqlQuery<RegistrationResultViewModel>("spGetAdvancedModuleRegistrationByIdentityCardExamPeriodId @IdentityCard, @ExamPeriodId", pars).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
