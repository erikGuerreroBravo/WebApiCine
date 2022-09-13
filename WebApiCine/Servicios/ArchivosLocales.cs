namespace WebApiCine.Servicios
{
    public class ArchivosLocales
    {
        public Task BorrarArchivo(string ruta, string contenedor)
        {
            return null;    
        }
        public Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta)
        {
            return null;
        }
        public Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            return null;
        }
    }
}
