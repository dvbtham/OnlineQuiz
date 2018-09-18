using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IRegistrationRepository
    {
        void InsertAdvancedModuleRegistration(InsertRegistrationViewModel vm);
        void InsertBasicRegistration(InsertRegistrationViewModel vm);
    }

    public class RegistrationRepository : RepositoryBase<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void InsertAdvancedModuleRegistration(InsertRegistrationViewModel vm)
        {
            try
            {
                var examinee = DbContext.Examinees.FirstOrDefault(x => x.IdentityCard == vm.IdentityCard);

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

        public void InsertBasicRegistration(InsertRegistrationViewModel vm)
        {
            try
            {
                var examinee = DbContext.Examinees.FirstOrDefault(x => x.IdentityCard == vm.IdentityCard);

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

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
