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
            CreateMap<ActorCreacionDto, Actor>().ForMember(x=>x.Foto , options => options.Ignore());
            CreateMap<ActorPatchDto, Actor>().ReverseMap();

            CreateMap<Pelicula,PeliculaDto>().ReverseMap();
            CreateMap<PeliculaCreacionDto, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros);
            CreateMap<PeliculaPatchDto,Pelicula>().ReverseMap();    

        }

        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDto peliculaCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if(peliculaCreacionDto.GenerosIds == null)
            {
                return resultado;
            }
            foreach (var id in peliculaCreacionDto.GenerosIds)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id});
            }
            return resultado;

        }




    }
}
