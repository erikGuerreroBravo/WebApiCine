using AutoMapper;
using WebApiCine.DTO;
using WebApiCine.Entidades;

namespace WebApiCine.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDto>().ReverseMap();
        }
    }
}
