using AutoMapper;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Profiles {

    public class MemosProfile : Profile {
        public MemosProfile () {

            //Source -> Target
            CreateMap<Memos, MemoReadDto> ();
            CreateMap<MemoCreateDto, Memos> ();
            CreateMap<MemoUpdateDto, Memos> ();
            CreateMap<Memos, MemoUpdateDto> ();
        }
    }
}