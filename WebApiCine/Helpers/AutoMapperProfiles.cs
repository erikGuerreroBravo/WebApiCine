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
            CreateMap<GeneroCreacionDto, Genero>();
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorCreacionDto, Actor>();
        }
    }
}
