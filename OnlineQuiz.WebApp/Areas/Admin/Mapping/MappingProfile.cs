using AutoMapper;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.WebApp.Areas.Admin.Models;

namespace OnlineQuiz.WebApp.Areas.Admin.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(m =>
            {
                CreateMap<QuestionViewModel, Question>();
                CreateMap<Examination, ExaminationViewModel>();
                CreateMap<ExaminationViewModel, Examination>()
                    .ForMember(x => x.ID, opt => opt.Ignore());
            });

        }
    }
}