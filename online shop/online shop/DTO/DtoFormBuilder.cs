using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace online_shop.DTO;

public class DtoFormBuilder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Success(new List<Filter>()); // Return empty list
            return Task.CompletedTask;
        }

        var json = valueProviderResult.Values;
        if (string.IsNullOrEmpty(json))
        {
            bindingContext.Result = ModelBindingResult.Success(new List<Filter>()); // Return empty list
            return Task.CompletedTask;
        }

        try
        {
            if (json.ToString().StartsWith("{"))
            {
                json = $"[{json}]";
            }
            
            var filters = JsonSerializer.Deserialize<List<Filter>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            bindingContext.Result = ModelBindingResult.Success(filters);
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid Filters format.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}