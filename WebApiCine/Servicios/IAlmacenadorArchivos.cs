namespace WebApiCine.Servicios
{
    public interface IAlmacenadorArchivos
    {
        public Task BorrarArchivo(string ruta, string contenedor);

        public Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType);

        public Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType);
        
    }
}
