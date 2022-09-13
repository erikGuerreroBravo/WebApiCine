namespace WebApiCine.DTO
{
    public class PaginacionDto
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistrosPorPagina = 10;
        private readonly int cantidadMaximaRegistrosPorPagina = 50;


        public int CantidadRegistrosPorPagina {
            get => cantidadRegistrosPorPagina;
            set {
                ///validamos la cantidad maxima de registros por pagina sea  igual a lo establecido
                cantidadRegistrosPorPagina = (value > cantidadRegistrosPorPagina)? cantidadMaximaRegistrosPorPagina: value;
            }
        }

    }
}
