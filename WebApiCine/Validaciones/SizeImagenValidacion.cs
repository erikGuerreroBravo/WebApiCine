using System.ComponentModel.DataAnnotations;

namespace WebApiCine.Validaciones
{
    public class SizeImagenValidacion: ValidationAttribute
    {
        private readonly int pesoMaximoMB;
        public SizeImagenValidacion(int pesoMaximoMB)
        {
            this.pesoMaximoMB = pesoMaximoMB;   
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if (formFile.Length > pesoMaximoMB * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {this.pesoMaximoMB} mb");
            }
            return ValidationResult.Success;    
        }
    }
}
