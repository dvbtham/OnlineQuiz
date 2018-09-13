using System;
using OnlineQuiz.Model.Entity;

namespace OnlineQuiz.Model.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        OnlineQuizDbContext Init();
    }
}