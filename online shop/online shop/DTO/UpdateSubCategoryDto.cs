using Microsoft.AspNetCore.Mvc;

namespace online_shop.DTO;

public record UpdateSubCategoryDto(string Title , string Slug , string Parent ,string Description , [ModelBinder(BinderType = typeof(DtoFormBuilder))] List<Filter> Filters ,  IFormFile? IconFile);