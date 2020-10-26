using AutoMapper;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Profiles {

    public class GuidelinesProfile : Profile {
        public GuidelinesProfile () {

            //Source -> Target
            CreateMap<Guidelines, GuidelineReadDto> ();
            CreateMap<GuidelineCreateDto, Guidelines> ();
            CreateMap<GuidelineUpdateDto, Guidelines> ();
            CreateMap<Guidelines, GuidelineUpdateDto> ();
        }
    }
}