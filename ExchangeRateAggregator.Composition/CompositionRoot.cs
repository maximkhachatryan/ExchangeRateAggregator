using ExchangeRateAggregator.ApplicationContracts.Contracts.Services.ApplicationServices;
using ExchangeRateAggregator.ApplicationContracts.Dtos;
using ExchangeRateAggregator.ApplicationServices;
using ExchangeRateAggregator.ApplicationServices.WebParsers;
using ExchangeRateAggregator.Domain.Contracts;
using ExchangeRateAggregator.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Composition
{
    public static class CompositionRoot
    {
        public static void RegisterDependencies(
            IServiceCollection services,
            IConfiguration configuration)
        {

            RegisterApplicationServices(services);

            RegisterWebParsers(services);

            RegisterSqlRepositories(services);

            RegisterDataContext(services, configuration);

            RegisterHttpClient(services);
        }

        

        private static void RegisterApplicationServices(IServiceCollection service)
        {
            var applicationServices = Assembly.GetAssembly(typeof(BankCurrencyRateService))!
                .GetTypes()
                .Where(x => !x.IsInterface
                        && x.GetInterface(typeof(IApplicationService).Name) != null);

            foreach (var serviceType in applicationServices)
            {
                try
                {
                    var type = serviceType.UnderlyingSystemType;
                    service.AddScoped(type.GetInterface($"I{type.Name}")!, type);
                }
                catch (Exception e)
                {
                    throw new Exception($"{e?.Message}-{serviceType}");
                }
            }
        }

        private static void RegisterWebParsers(IServiceCollection services)
        {
            services.AddSingleton<WebParserFactory>();
        }

        private static void RegisterSqlRepositories(
            IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        }

        private static void RegisterDataContext(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ExchangeRateAggregatorDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString(nameof(ExchangeRateAggregatorDbContext)));
            }, ServiceLifetime.Scoped);
        }

        private static void RegisterHttpClient(
            IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}
