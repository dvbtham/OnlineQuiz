using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExamineeRepository
    {
        ExamineeViewModel Insert(ExamineeViewModel examinee);
        ExamineeViewModel Update(ExamineeViewModel examinee);
        ExamineeViewModel FindByIdentityCard(string identityCard);
        ExamineeViewModel InsertOrUpdate(ExamineeViewModel examinee);
    }

    public class ExamineeRepository : RepositoryBase<Examinee>, IExamineeRepository
    {
        public ExamineeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ExamineeViewModel FindByIdentityCard(string identityCard)
        {
            var examinee = GetSingleByCondition(x => x.IdentityCard == identityCard);
            if (examinee != null)
            {
                var vm = new ExamineeViewModel()
                {
                    ID = examinee.ID,
                    FirstName = examinee.FirstName,
                    LastName = examinee.LastName,
                    IdentityCard = examinee.IdentityCard,
                    DateOfBirth = examinee.DateOfBirth.Value,
                    Gender = examinee.Gender.Value,
                    Email = examinee.Email,
                    Mobile = examinee.Mobile
                };
                return vm;
            }
            return new ExamineeViewModel();
        }

        public ExamineeViewModel Insert(ExamineeViewModel examineeVm)
        {
            try
            {
                var entity = new Examinee
                {
                    ID = Guid.NewGuid(),
                    FirstName = examineeVm.FirstName,
                    LastName = examineeVm.LastName,
                    FullName = examineeVm.LastName + " " + examineeVm.FirstName,
                    DateOfBirth = examineeVm.DateOfBirth,
                    Gender = examineeVm.Gender,
                    IdentityCard = examineeVm.IdentityCard,
                    Mobile = examineeVm.Mobile,
                    Email = examineeVm.Email
                };

                var vm = Add(entity);
                examineeVm.ID = vm.ID;

                return examineeVm;
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }

        public ExamineeViewModel InsertOrUpdate(ExamineeViewModel examinee)
        {
            if (GetSingleByCondition(x => x.IdentityCard == examinee.IdentityCard) == null)
            {
                return Insert(examinee);
            }
            else
                return Update(examinee);
        }

        public ExamineeViewModel Update(ExamineeViewModel examineeVm)
        {
            try
            {
                var entity = GetSingleByCondition(x => x.IdentityCard == examineeVm.IdentityCard);

                entity.FirstName = examineeVm.FirstName;
                entity.LastName = examineeVm.LastName;
                entity.FullName = examineeVm.LastName + " " + examineeVm.FirstName;
                entity.DateOfBirth = examineeVm.DateOfBirth;
                entity.Gender = examineeVm.Gender;
                entity.IdentityCard = examineeVm.IdentityCard;
                entity.Mobile = examineeVm.Mobile;
                entity.Email = examineeVm.Email;

                Update(entity);

                return examineeVm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
