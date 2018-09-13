using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Repositories;
using System.Collections.Generic;

namespace OnlineQuiz.Service.Services
{
    public interface INoteService
    {
        IEnumerable<KeyValuePair> GetAll();
    }

    public class NoteService : INoteService
    {
        private readonly INoteRepository noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }
        public IEnumerable<KeyValuePair> GetAll()
        {
            return noteRepository.GetAll();
        }
    }
}
