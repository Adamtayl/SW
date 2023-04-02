using Smartwyre.DeveloperTest.Services.RebateCalculationServices;
using Smartwyre.DeveloperTest.Tests.TestData;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedCashAmountCalculationServiceTests
{
    private readonly FixedCashAmountCalculationService _fixedCashAmountCalculationService = new();

    [Theory]
    [ClassData(typeof(FixedCashAmountCalculationServiceTestData))]
    public void CorrectlyCalculatesRebate(decimal rebateAmount, decimal expectedRebate)
    {
        // Arrange
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var rebate = new Rebate { Amount = rebateAmount };
        var calculateRebateRequest = new CalculateRebateRequest();

        // Act
        var result = _fixedCashAmountCalculationService.CalculateRebate(product, rebate, calculateRebateRequest);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(expectedRebate, result.RebateAmount);
    }

}