using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCine.DTO;
using WebApiCine.Entidades;
using WebApiCine.Helpers;
using WebApiCine.Servicios;
//cargamos la libreria de linq dinamico
using System.Linq.Dynamic.Core;


namespace WebApiCine.Controllers
{
    /// <summary>
    /// Se agrega el controlador peliculas
    /// </summary>
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        //creacion del contenedor o carpeta de archivos donde se va a almacenar las fotos
        private readonly string contenedor = "peliculas";
        private readonly ILogger logger;


        public PeliculasController(ApplicationDbContext _context, IMapper _mapper,
            IAlmacenadorArchivos _almacenadorArchivos, ILogger<PeliculasController> _logger): base(_context,_mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.almacenadorArchivos = _almacenadorArchivos;
            this.logger = _logger;
        }
        [HttpGet]
        public async Task<ActionResult<PeliculasIndexDto>> Get()
        {
            //solo 5 peliculas en la consulta
            var top = 5;
            //fecha del dia actual
            var hoy = DateTime.Today;
            //consulta de filtrado por fechas de estreno  y solo traemos los 5 estenos
            var proximosEstrenos = await context.Peliculas
                .Where(x => x.FechaEstreno > hoy)
                .OrderBy(x => x.FechaEstreno)
                .Take(top)
                .ToListAsync();
            var enCines = await context.Peliculas
                                .Where(x => x.EnCines)
                                .Take(top)
                                .ToListAsync();
            var resultado = new PeliculasIndexDto();
            //agregamos los futuros estrenos
            resultado.FuturosEstrenos = mapper.Map<List<PeliculaDto>>(proximosEstrenos);
            //agregamos los estrenos que aun estan en cines
            resultado.EnCines = mapper.Map<List<PeliculaDto>>(enCines);
            return resultado;

            //var peliculas = await context.Peliculas.ToListAsync();
            //return mapper.Map<List<PeliculaDto>>(peliculas);
        }
        
        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDetallesDto>> Get(int id)
        {
            var pelicula = await context.Peliculas
                .Include(x=>x.PeliculasActores)
                .ThenInclude(x=>x.Actor)
                .Include(x=> x.PeliculasGeneros)
                .ThenInclude(x=> x.Genero)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula == null)
            {
                return NotFound();  
            }
            pelicula.PeliculasActores = pelicula.PeliculasActores.OrderBy(x => x.Orden).ToList();

            return mapper.Map<PeliculaDetallesDto>(pelicula);
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDto>>> Filtrar([FromQuery]FiltroPeliculasDto filtroPeliculasDto )
        {
            ///ejecutamos la ejecucion diferida de la siguiente manera
            var peliculasQueryable = context.Peliculas.AsQueryable();

            if (!string.IsNullOrEmpty(filtroPeliculasDto.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Titulo.Contains(filtroPeliculasDto.Titulo));
            }
            if (filtroPeliculasDto.EnCines)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.EnCines);
            }
            if (filtroPeliculasDto.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(x => x.FechaEstreno > hoy);
            }
            if (filtroPeliculasDto.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.PeliculasGeneros.Select(y => y.GeneroId)
                .Contains(filtroPeliculasDto.GeneroId));
                    
            }
            if (!string.IsNullOrEmpty(filtroPeliculasDto.CampoOrdenar))
            {
                var tipoOrden = filtroPeliculasDto.OrdenAscendente ? "ascending" : "descending";
                try
                {
                    peliculasQueryable = peliculasQueryable.OrderBy($"{filtroPeliculasDto.CampoOrdenar} {tipoOrden}");
                }
                catch (Exception ex)
                {

                    logger.LogError(ex.Message, ex);
                }
                
                //if (filtroPeliculasDto.CampoOrdenar == "titulo")
                //{
                //    if (filtroPeliculasDto.OrdenAscendente)
                //    {
                //        peliculasQueryable = peliculasQueryable.OrderBy(x => x.Titulo);
                //    }
                //    else
                //    {
                //        peliculasQueryable = peliculasQueryable.OrderByDescending(x => x.Titulo);
                //    }
                //}
            }

            await HttpContext.InsertarParametrosPaginacion(peliculasQueryable, filtroPeliculasDto.CantidadRegistrosPorPagina);

            var peliculas = await peliculasQueryable.Paginar(filtroPeliculasDto.Paginacion).ToListAsync();
            return mapper.Map<List<PeliculaDto>>(peliculas);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDto peliculaCreacionDto)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDto);
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    pelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, peliculaCreacionDto.Poster.ContentType);
                }
                
            }
            AsignacionActores(pelicula);
            context.Add(pelicula);
            await context.SaveChangesAsync();
            var peliculaDto = mapper.Map<PeliculaDto>(pelicula);
            return new CreatedAtRouteResult("obtenerPelicula", new { id = pelicula.Id }, peliculaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] PeliculaCreacionDto peliculaCreacionDto)
        {
            var peliculaDB = await context.Peliculas
                .Include(x=>x.PeliculasActores)
                .Include(x=>x.PeliculasGeneros)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (peliculaDB == null) { return NotFound(); }

            peliculaDB = mapper.Map(peliculaCreacionDto, peliculaDB);
            if (peliculaCreacionDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDto.Poster.FileName);
                    peliculaDB.Poster = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, peliculaDB.Poster,
                        peliculaCreacionDto.Poster.ContentType);
                }
            }
            AsignacionActores(peliculaDB);
            await context.SaveChangesAsync();
            return NoContent();


        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDto> patchDocument)
        {
            //    if (patchDocument == null)
            //    {
            //        return BadRequest();
            //    }
            //    var peliculaDB = context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            //    if (peliculaDB == null)
            //    {
            //        return NotFound();
            //    }
            //    var peliculaDto = mapper.Map<PeliculaPatchDto>(peliculaDB);
            //    patchDocument.ApplyTo(peliculaDto, ModelState);
            //    var esValido = TryValidateModel(peliculaDto);
            //    if (!esValido)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //    mapper.Map(peliculaDto, peliculaDB);  
            //    await context.SaveChangesAsync();
            //    return NoContent();
            return await Patch<Pelicula, PeliculaPatchDto>(id,patchDocument);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var existencia = await context.Peliculas.AnyAsync(x => x.Id == id);
            //if (!existencia)
            //{
            //    return NotFound();
            //}
            //context.Remove(new Pelicula() { Id = id });
            //await context.SaveChangesAsync();
            //return NoContent();
            return await Delete<Pelicula>(id);
        }


        private void AsignacionActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActores != null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i;
                }
            }
        }

        


    }
}
