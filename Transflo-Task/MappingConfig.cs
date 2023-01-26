using AutoMapper;
using Transflo_Task.Models;
using Transflo_Task.Models.Dto;

namespace Transflo_Task
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, Driver>();

            CreateMap<Driver, DriverCreateDTO>().ReverseMap();
            CreateMap<Driver, DriverUpdateDTO>().ReverseMap();

        }
    }
}
