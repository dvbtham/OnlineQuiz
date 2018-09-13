using AutoMapper;
using OnlineQuiz.Model.Entity;
using System.Collections.Generic;

namespace OnlineQuiz.WebApp.Areas.Admin.Models
{
    public static class MappingExtensions
    {
        public static IEnumerable<ExaminationViewModel> FromList(this ExaminationViewModel dest, IEnumerable<Examination> source)
        {
            return Mapper.Map<IEnumerable<Examination>, IEnumerable<ExaminationViewModel>>(source);
        }
       
    }
}