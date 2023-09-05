using Microsoft.OpenApi.Models;

namespace ExchangeRateAggregator.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExchangeRateAggregator", Version = "v1" });
            });
        }
    }
}
