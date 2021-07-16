using AutoMapper;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Profiles
{

    public class PrescriptiontoolsProfile : Profile
    {
        public PrescriptiontoolsProfile()
        {

            //Source -> Target
            CreateMap<Prescriptiontools, PrescriptiontoolReadDto>();
            CreateMap<PrescriptiontoolCreateDto, Prescriptiontools>();
            CreateMap<PrescriptiontoolUpdateDto, Prescriptiontools>();
            CreateMap<Prescriptiontools, PrescriptiontoolUpdateDto>();
        }
    }
}