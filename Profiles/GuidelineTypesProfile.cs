using AutoMapper;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Profiles {

    public class GuidelineTypeProfile : Profile {
        public GuidelineTypeProfile () {

            //Source -> Target
            CreateMap<Guidelinetypes, GuidelineTypeReadDto> ();
            CreateMap<GuidelineTypeCreateDto, Guidelinetypes> ();
            CreateMap<GuidelineTypeUpdateDto, Guidelinetypes> ();
            CreateMap<Guidelinetypes, GuidelineTypeUpdateDto> ();
        }
    }
}