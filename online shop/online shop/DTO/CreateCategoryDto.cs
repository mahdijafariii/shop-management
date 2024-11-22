using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace online_shop.DTO;

//[ModelBinder(BinderType = typeof(DtoFormBuilder))]  
public record CreateCategoryDto(string Title , string Slug , ObjectId? Parent ,string Description , [ModelBinder(BinderType = typeof(DtoFormBuilder))] List<Filter> Filters ,  IFormFile? IconFile);