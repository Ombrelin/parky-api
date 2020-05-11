using AutoMapper;
using WebApplication.Models;
using WebApplication.Models.DTOs;

namespace WebApplication.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
        }
    }
}