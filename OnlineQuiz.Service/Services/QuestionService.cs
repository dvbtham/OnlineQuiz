using OnlineQuiz.Model.Repositories;
using OnlineQuiz.Model.Entity;
using System;
using System.Collections.Generic;

namespace OnlineQuiz.Service.Services
{
    public interface IQuestionService : IBaseService<Question>
    {
        void AddRange(List<Question> questions);
        Question GetByTitle(string title);
    }

    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public Question Add(Question entity)
        {
            return questionRepository.Add(entity);
        }

        public void AddRange(List<Question> questions)
        {
            questionRepository.AddRange(questions);
        }
        public Question Delete(int id)
        {
            return questionRepository.Delete(id);
        }

        public Question Delete(Guid? id)
        {
            return questionRepository.Delete(id);
        }

        public Question FindById(int id)
        {
            return questionRepository.GetSingleById(id);
        }

        public Question FindById(Guid? id)
        {
            return questionRepository.GetSingleById(id);
        }

        public IEnumerable<Question> GetAll(string include = null)
        {
            if (string.IsNullOrEmpty(include))
                return questionRepository.GetAll();
            return questionRepository.GetAll(new[] { "QuestionModule", "QuestionClassification" });
        }

        public Question GetByTitle(string title)
        {
            return questionRepository.GetSingleByCondition(x => x.QuestionContent == title);
        }

        public void Update(Question entity)
        {
            questionRepository.Update(entity);
        }
    }
}
