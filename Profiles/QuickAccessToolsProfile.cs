using AutoMapper;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Profiles
{

    public class QuickAccessToolsProfile : Profile
    {
        public QuickAccessToolsProfile()
        {
            //Source -> Target
            CreateMap<Quickaccesstools, QuickAccessToolReadDto>();
            CreateMap<QuickAccessToolCreateDto, Quickaccesstools>();
            CreateMap<QuickAccessToolUpdateDto, Quickaccesstools>();
            CreateMap<Quickaccesstools, QuickAccessToolUpdateDto>();
        }
    }
}