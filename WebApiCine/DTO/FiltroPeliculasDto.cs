namespace WebApiCine.DTO
{
    public class FiltroPeliculasDto
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;
        public PaginacionDto Paginacion
        {
            get
            {
                return new PaginacionDto() { Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina };

            }
        }
        public string Titulo { get; set; }
    }
}
