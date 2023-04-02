using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculationServices;

public class RebateCalculationServiceFactory : IRebateCalculationServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RebateCalculationServiceFactory(IServiceProvider serviceProvider) 
        => _serviceProvider = serviceProvider;

    public IRebateCalculationService Resolve(IncentiveType incentiveType) =>
        incentiveType switch
        {
            IncentiveType.FixedRateRebate =>
                _serviceProvider.GetRequiredService<FixedRateRebateCalculationService>(),
            IncentiveType.AmountPerUom => _serviceProvider.GetRequiredService<AmountPerUomCalculationService>(),
            IncentiveType.FixedCashAmount =>
                _serviceProvider.GetRequiredService<FixedCashAmountCalculationService>(),
            _ => throw new ArgumentOutOfRangeException(nameof(incentiveType), incentiveType, null)
        };
}