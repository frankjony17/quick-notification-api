using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Quick.Notification.Api.Command.Configurations.Extensions
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IConfiguration config, IApiVersionDescriptionProvider provider)
        {
            var useSwagger = config.GetValue<bool>("UseSwagger");

            if (!useSwagger)
            {
                return app;
            }

            return app
                .UseSwagger()
                .UseSwaggerUI(options => provider.ApiVersionDescriptions
                    .ToList()
                    .ForEach(act =>
                        options.SwaggerEndpoint($"/swagger/{act.GroupName}/swagger.json", act.GroupName.ToUpper())));
        }
    }
}

