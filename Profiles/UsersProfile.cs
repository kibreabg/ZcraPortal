using AutoMapper;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Profiles {

    public class UsersProfile : Profile {
        public UsersProfile () {

            //Source -> Target
            CreateMap<Users, UserReadDto> ();
            CreateMap<UserCreateDto, Users> ();
            CreateMap<UserUpdateDto, Users> ();
            CreateMap<Users, UserUpdateDto> ();
        }
    }
}