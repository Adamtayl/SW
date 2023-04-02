using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Runner;
using Smartwyre.DeveloperTest.Services.RebateCalculationServices;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateCalculationServiceFactoryTests
{
    private readonly IRebateCalculationServiceFactory _rebateCalculationServiceFactory;

    public RebateCalculationServiceFactoryTests()
    {

        var serviceCollection = CompositionRoot.Prepare();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _rebateCalculationServiceFactory = serviceProvider.GetRequiredService<IRebateCalculationServiceFactory>();
    }

    [Theory]
    [InlineData(IncentiveType.AmountPerUom, typeof(AmountPerUomCalculationService))]
    [InlineData(IncentiveType.FixedRateRebate, typeof(FixedRateRebateCalculationService))]
    [InlineData(IncentiveType.FixedCashAmount, typeof(FixedCashAmountCalculationService))]
    public void ResolveReturnsCorrectRebateCalculationService(IncentiveType incentive, Type expectedType)
    {
        // Act
        var calculationService = _rebateCalculationServiceFactory.Resolve(incentive);

        // Assert
        Assert.IsType(expectedType, calculationService);
    }

    [Fact]
    public void ResolveThrowsWhenPassedIncorrectIncentiveType()
    {
        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _rebateCalculationServiceFactory.Resolve((IncentiveType)int.MaxValue));
    }
}