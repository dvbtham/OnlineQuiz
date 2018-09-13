using OnlineQuiz.Common;
using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace OnlineQuiz.Model.Repositories
{
    public interface IAccountRepository     {         ResponseBase Edit(UserEditViewModel viewModel);         LoginResult CheckLogin(string examineeId, string password);         AttendanceViewModel GetAttendanceInfo(string id);         ExaminateInfoViewModel GetBasicExamInfo(string id);
        ExaminateInfoViewModel GetAdvcExamInfo(string id, string title);
        IEnumerable<ExaminateInfoViewModel> GetAdvcModules(string id);     }      public class AccountRepository : RepositoryBase<Question>, IAccountRepository     {         public AccountRepository(IDbFactory dbFactory) : base(dbFactory)         {         }

        public LoginResult CheckLogin(string examineeId, string password)
        {
            var loginResult = new LoginResult();
            var encryptPass = new PasswordManger().Encryption(password);

            var data = DbContext.IDExamineeRegistrations
                .FirstOrDefault(x => x.IDExaminee == examineeId && x.Password == encryptPass);

            loginResult.Status = data != null;

            if (!loginResult.Status)
                return loginResult;

            var examID = data.Registration.Examinee.ID;

            loginResult.UserInfo = DbContext.Examinees
                .Select(x => new KeyValuePair
                {
                    Key = x.Registrations
                    .Select(y => y.IDExamineeRegistrations.FirstOrDefault(c => c.IDExaminee == examineeId && c.Status.Value)).FirstOrDefault().IDExaminee,
                    Value = x.LastName + " " + x.FirstName
                }).FirstOrDefault(x => x.Key == examineeId);

            return loginResult;
        }

        public AttendanceViewModel GetAttendanceInfo(string id)
        {
            try
            {
                var vm = new AttendanceViewModel
                {
                    Examinee = GetExaminee(id),
                    ExaminationCouncil = GetExaminationCouncil(id)
                };
                return vm;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// chưa làm cái này
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ExamineeViewModel GetExaminee(string id)
        {
            var idreg = DbContext.IDExamineeRegistrations
                .Include(x => x.Registration)
                .FirstOrDefault(x => x.IDExaminee == id);

            var examineeVm = DbContext.Examinees
                   .Select(x => new ExamineeViewModel
                   {
                       ID = x.ID.ToString(),
                       ExamineeID = id,
                       FullName = x.LastName + " " + x.FirstName,
                       DateOfBirth = x.DateOfBirth.Value,
                       Gender = x.Gender.Value ? "Nam" : "Nữ",
                       CMND = x.IdentityCard

                   }).FirstOrDefault(x => x.ID == idreg.Registration.ExamineeID.ToString());
            return examineeVm;
        }

        private ExaminationCouncilViewModel GetExaminationCouncil(string id)
        {
            var vm = new ExaminationCouncilViewModel
            {
                Modules = new SelectList(DbContext.InformationTechnologySkills.OrderByDescending(x => x.ID), "ID", "Name"),
                QuestionModules = new SelectList(DbContext.QuestionModules, "ID", "Name")
            };

            return vm;
        }

        public ExaminateInfoViewModel GetBasicExamInfo(string id)
        {
            var pars = new SqlParameter("@IDExaminee", id);
            return DbContext.Database.SqlQuery<ExaminateInfoViewModel>("spGetDangKyThiChuanCNTTCoBan @IDExaminee", pars).FirstOrDefault();
        }

        public ExaminateInfoViewModel GetAdvcExamInfo(string id, string title)
        {
            var pars = new SqlParameter[]
            {
                new SqlParameter("@IDExaminee", id),
                new SqlParameter("@Title", title)
            };

            return DbContext.Database.SqlQuery<ExaminateInfoViewModel>("spGetDangKyThiChuanCNTTNangCaoByTitle @IDExaminee, @Title", pars).FirstOrDefault();
        }

        public IEnumerable<ExaminateInfoViewModel> GetAdvcModules(string id)
        {
            var pars = new SqlParameter("@IDExaminee", id);
            return DbContext.Database.SqlQuery<ExaminateInfoViewModel>("spGetDangKyThiChuanCNTTNangCao @IDExaminee", pars);
        }

        public ResponseBase Edit(UserEditViewModel viewModel)
        {
            try
            {
                var idreg = DbContext.IDExamineeRegistrations.FirstOrDefault(x => x.IDExaminee == viewModel.IDExaminee && x.Status.Value == true);
                var examinee = DbContext.Examinees.FirstOrDefault(x => x.ID == idreg.Registration.ExamineeID);
                if (examinee == null)
                    return new ResponseBase() { Message = "Dữ liệu không tìm thấy" };
                var fullname = viewModel.FullName.Split(' ');
                var lastName = "";
                for (int i = 0; i < fullname.Length - 1; i++)
                {
                    lastName += fullname[i] + " ";
                }
                examinee.FullName = viewModel.FullName;
                examinee.LastName = lastName;
                examinee.FirstName = fullname.Last();
                examinee.IdentityCard = viewModel.CMND;
                examinee.DateOfBirth = viewModel.DOB;
                examinee.Gender = viewModel.Gender;

                DbContext.Entry(examinee).State = System.Data.Entity.EntityState.Modified;

                DbContext.SaveChanges();

                return new ResponseBase() { Status = true, Model = viewModel, Message = "Cập nhật thành công" };
            }
            catch (System.Exception e)
            {
                return new ResponseBase() { Message = "Có lỗi xảy ra", StackTrace = e.StackTrace.ToString() };
            }
        }
    }
}
