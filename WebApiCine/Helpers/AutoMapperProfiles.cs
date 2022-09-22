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

            CreateMap<Pelicula, PeliculaDetallesDto>()
                .ForMember(x => x.Generos, options => options.MapFrom());

            CreateMap<Pelicula,PeliculaDto>().ReverseMap();
            CreateMap<PeliculaCreacionDto, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x=> x.PeliculasActores, options => options.MapFrom(MapPeliculasActores));
            CreateMap<PeliculaPatchDto,Pelicula>().ReverseMap();    

        }
        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDto peliculaCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if (peliculaCreacionDto.Actores == null)
            {
                return resultado;
            }
            foreach (var actor in peliculaCreacionDto.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId, Personaje = actor.Personaje });
            }
            return resultado;

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

        private List<GeneroDto> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDto peliculaDetallesDto)
        {
            
        }


    }
}
