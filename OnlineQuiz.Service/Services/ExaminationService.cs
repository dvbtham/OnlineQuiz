using OnlineQuiz.Model.Repositories;
using OnlineQuiz.Model.Entity;
using System;
using System.Collections.Generic;

namespace OnlineQuiz.Service.Services
{
    public interface IExaminationService : IBaseService<Examination>
    {
    }

    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;

        public ExaminationService(IExaminationRepository examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }
        public Examination Add(Examination entity)
        {
            return _examinationRepository.Add(entity);
        }

        public Examination Delete(int id)
        {
            return _examinationRepository.Delete(id);
        }
        public Examination Delete(Guid? id)
        {
            return _examinationRepository.Delete(id);
        }

        public Examination FindById(int id)
        {
            return _examinationRepository.GetSingleById(id);
        }

        public Examination FindById(Guid? id)
        {
            return _examinationRepository.GetSingleById(id);
        }


        public IEnumerable<Examination> GetAll(string include = null)
        {
            if (string.IsNullOrEmpty(include))
                return _examinationRepository.GetAll();
            return _examinationRepository.GetAll(new[] { include });
        }

        public void Update(Examination entity)
        {
            _examinationRepository.Update(entity);
        }
    }
}
