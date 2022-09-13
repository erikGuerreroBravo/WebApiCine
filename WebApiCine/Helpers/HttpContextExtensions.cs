namespace WebApiCine.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable,
            int cantidadRegistrosPorPagina)
        {
     
        }
    }
}
