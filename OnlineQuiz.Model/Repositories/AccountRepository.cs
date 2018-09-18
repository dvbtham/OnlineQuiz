using OnlineQuiz.Common;
using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IAccountRepository     {         ResponseBase Edit(RequestEditViewModel viewModel);         LoginResult CheckLogin(string examineeId, string password);         AttendanceViewModel GetAttendanceInfo(string id);     }      public class AccountRepository : RepositoryBase<Question>, IAccountRepository     {         public AccountRepository(IDbFactory dbFactory) : base(dbFactory)         {         }

        public LoginResult CheckLogin(string examineeId, string password)
        {
            var loginResult = new LoginResult();
            var encryptPass = new PasswordManger().Encryption(password);

            var pars = new SqlParameter[]
           {
                new SqlParameter("@IDExaminee", examineeId),
                new SqlParameter("@Password", encryptPass)
           };

            var user = DbContext.Database.SqlQuery<ExamineeViewModel>("spLogin @IDExaminee, @Password", pars).FirstOrDefault();

            loginResult.Status = user != null;

            if (user != null)
            {
                loginResult.UserInfo = user;
            }

            return loginResult;
        }

        public AttendanceViewModel GetAttendanceInfo(string id)
        {
            try
            {
                var pars = new SqlParameter("@IDExaminee", id);
                var examineeId = id.Substring(0, 2);
                if (examineeId == "CB")
                {
                    var vm = DbContext.Database.SqlQuery<AttendanceViewModel>("spGetDangKyThiChuanCNTTCoBan @IDExaminee", pars).FirstOrDefault();
                    return vm;
                }
                else
                {
                    var vm = DbContext.Database.SqlQuery<AttendanceViewModel>("spGetDangKyThiChuanCNTTNangCao @IDExaminee", pars).FirstOrDefault();
                    return vm;
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public ResponseBase Edit(RequestEditViewModel viewModel)
        {
            try
            {
                var examineeId = Guid.NewGuid();
                if (viewModel.IDExaminee.Contains("CB"))
                {
                    examineeId = DbContext.IDExamineeRegistrations.Include(x => x.Registration)
                        .FirstOrDefault(x => x.IDExaminee == viewModel.IDExaminee).Registration.ExamineeID.Value;
                }
                else
                {
                    examineeId = DbContext.AdvancedModuleRegistrations.Include(x => x.Registration)
                        .FirstOrDefault(x => x.IDExaminee == viewModel.IDExaminee).Registration.ExamineeID.Value;
                }

                var examinee = DbContext.Examinees.FirstOrDefault(x => x.ID == examineeId);

                if (examinee == null)
                    return new ResponseBase() { Message = "Dữ liệu không tìm thấy" };

                examinee.Remark = viewModel.Note;

                DbContext.Entry(examinee).State = EntityState.Modified;

                DbContext.SaveChanges();

                return new ResponseBase() { Status = true, Model = viewModel, Message = "Yêu cầu cập nhật thông tin đã được gửi" };
            }
            catch (System.Exception e)
            {
                return new ResponseBase() { Message = "Có lỗi xảy ra", StackTrace = e.StackTrace.ToString() };
            }
        }
    }
}
