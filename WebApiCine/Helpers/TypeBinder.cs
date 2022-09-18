using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WebApiCine.Helpers
{
    public class TypeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);
            //verificamos si la propiedad realmente tiene un elemento enlazado.
            if (proveedorDeValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<List<int>>(proveedorDeValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor invalido para tipo List<int>");
            }
            return Task.CompletedTask;
        }
    }
}
