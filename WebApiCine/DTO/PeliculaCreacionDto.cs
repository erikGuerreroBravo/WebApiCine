using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApiCine.Helpers;
using WebApiCine.Validaciones;

namespace WebApiCine.DTO
{
    public class PeliculaCreacionDto: PeliculaPatchDto
    {
        
        [SizeImagenValidacion(pesoMaximoMB: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }
        //es necesario utilizar un modelBinder para poder bindear todas las propiedades dentro del modelo 
        //Ids Asociados de los generos a las peliculas
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIds { get; set; }
        [ModelBinder(BinderType =typeof(TypeBinder<List<ActorPeliculasCreacionDto>>))]
        public List<ActorPeliculasCreacionDto> Actores { get; set; }


    }
}
