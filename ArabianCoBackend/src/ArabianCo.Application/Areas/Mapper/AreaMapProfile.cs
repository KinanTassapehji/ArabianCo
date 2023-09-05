using AutoMapper;
using ArabianCo.Areas.Dto;
using ArabianCo.Domain.Areas;

namespace ArabianCo.Areas.Mapper
{
    public class AreaMapProfile : Profile
    {
        public AreaMapProfile()
        {
            CreateMap<CreateAreaDto, Area>();
            CreateMap<CreateAreaDto, AreaDto>();
            CreateMap<AreaDto, Area>();
            CreateMap<UpdateAreaDto, Area>();
            CreateMap<LiteAreaDto, Area>();
        }
    }
}
