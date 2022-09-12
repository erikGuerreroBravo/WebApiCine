using System.ComponentModel.DataAnnotations;

namespace WebApiCine.Validaciones
{
    public class TipoArchivoValidacion: ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public TipoArchivoValidacion(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }
        public TipoArchivoValidacion()
        {

        }


    }
}
