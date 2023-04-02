using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.RebateCalculationServices;
using Smartwyre.DeveloperTest.Services.RebateServices;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Composition;

public static class RebateServiceModule
{
    public static IEnumerable<ServiceDescriptor> AddRebateServiceDescriptors(this IServiceCollection services)
    {
        services.AddScoped<IRebateService, RebateService>();
        services.AddScoped<IRebateCalculationServiceFactory, RebateCalculationServiceFactory>();

        services
            .AddScoped<FixedRateRebateCalculationService>()
            .AddScoped<IRebateCalculationService, FixedRateRebateCalculationService>(o => o.GetRequiredService<FixedRateRebateCalculationService>());

        services
            .AddScoped<AmountPerUomCalculationService>()
            .AddScoped<IRebateCalculationService, AmountPerUomCalculationService>(o => o.GetRequiredService<AmountPerUomCalculationService>());

        services
            .AddScoped<FixedCashAmountCalculationService>()
            .AddScoped<IRebateCalculationService, FixedCashAmountCalculationService>(o => o.GetRequiredService<FixedCashAmountCalculationService>());

        services.AddScoped<IRebateDataStore, RebateDataStore>();
        services.AddScoped<IProductDataStore, ProductDataStore>();

        return services;
    }
}
