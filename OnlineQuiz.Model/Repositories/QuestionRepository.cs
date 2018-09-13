﻿using OnlineQuiz.Model.Infrastructure; using OnlineQuiz.Model.Entity; using System.Collections.Generic;  namespace OnlineQuiz.Model.Repositories {     public interface IQuestionRepository : IRepository<Question>     {         void AddRange(List<Question> questions);     }      public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository     {         public QuestionRepository(IDbFactory dbFactory) : base(dbFactory)         {         }          public void AddRange(List<Question> questions)         {             DbContext.Questions.AddRange(questions);         }     } }