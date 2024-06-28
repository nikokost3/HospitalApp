using AutoMapper;
using HospitalApp.Data;
using HospitalApp.Models;
using HospitalApp.DTO;

namespace HospitalApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserPatchDTO>().ReverseMap();
            CreateMap<User, UserSignupDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
