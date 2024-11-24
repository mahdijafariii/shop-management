using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace online_shop.DTO;

public record CreateSubCategoryDto(string Title , string Slug , string Parent ,string Description , [ModelBinder(BinderType = typeof(DtoFormBuilder))] List<Filter> Filters);