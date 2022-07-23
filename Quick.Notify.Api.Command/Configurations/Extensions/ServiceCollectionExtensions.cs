using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quick.Notify.Api.Command.Filters;
using Quick.Notify.Domain.Configurations.Models;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Quick.Notify.Api.Command.Configurations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddControllersWithJsonOptions(this IServiceCollection services)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            JsonConvert.DefaultSettings = () => jsonSettings;

            services.AddControllers(options =>
            {
                options.Filters.Add<ContextFilter>();
                options.Filters.Add<ExceptionFilter>();
            })
                    .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = ctx =>
                    {
                        var errors = ctx.ModelState.Keys
                            .Select(x => new { Key = x, Values = ctx.ModelState[x] })
                            .Select(x => new InnerError
                            {
                                Title = $"validation error on field {x.Key}".Replace(" Data.", " "),
                                Detail = string.Join(" | ", x.Values.Errors.Select(err => err.ErrorMessage)),
                                Status = ((int)HttpStatusCode.BadRequest).ToString()
                            });

                        return new BadRequestObjectResult(new Error { Errors = errors });
                    })
                    .AddJsonSerializerOptions()
                    .AddNewtonsoftJson(jsonOptions =>
                    {
                        jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            return services;
        }

        private static IMvcBuilder AddJsonSerializerOptions(this IMvcBuilder mvcBuilder)
            => mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
    }
}

