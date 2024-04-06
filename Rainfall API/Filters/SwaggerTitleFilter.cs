using System.Reflection;
using Microsoft.OpenApi.Models;
using Rainfall_API.Models.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rainfall_API.Filters
{
    public class SwaggerTitleFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.GetCustomAttribute<TitleAttribute>() is TitleAttribute titleAttribute)
            {
                schema.Title = titleAttribute.Title;
            }
        }
    }
}