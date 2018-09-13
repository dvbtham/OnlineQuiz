namespace OnlineQuiz.Model.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}