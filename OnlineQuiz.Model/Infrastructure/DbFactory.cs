using OnlineQuiz.Model.Entity;

namespace OnlineQuiz.Model.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private OnlineQuizDbContext dbContext;

        public OnlineQuizDbContext Init()
        {
            return dbContext ?? (dbContext = new OnlineQuizDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}