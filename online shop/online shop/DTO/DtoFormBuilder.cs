using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace online_shop.DTO;

public class DtoFormBuilder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }
        var objRepresentation = valueProviderResult.FirstValue;
        
        if (string.IsNullOrEmpty(objRepresentation))
        {
            return Task.CompletedTask;
        }
        
        try
        {
            var result = JsonSerializer.Deserialize(
                objRepresentation,
                bindingContext.ModelType,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch (System.Exception)
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}