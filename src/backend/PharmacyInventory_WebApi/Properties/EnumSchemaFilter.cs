using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PharmacyInventory_WebApi.Properties
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum = Enum.GetNames(context.Type)
                .Select(name => new OpenApiString(name))
                    .ToList<IOpenApiAny>();
                schema.Type = "string"; // Set the enum type to string
            }
        }
    }
}
