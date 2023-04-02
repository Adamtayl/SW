using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Runner;
using Smartwyre.DeveloperTest.Types;
using Xunit;
using Smartwyre.DeveloperTest.Tests.TestData;
using Smartwyre.DeveloperTest.Services.RebateServices;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    [Fact]
    public void CalculateReturnsFailureIfNoRebateExists()
    {
        // Arrange

        // Given a bit more time I'd normally write a test wrapper around `CompositionRoot.Prepare` adding in test fakes
        // for calls to external service and to provide an easy way to overwrite dependencies
        var serviceCollection = CompositionRoot.Prepare();

        var mockRebateDataStore = Mock.Of<IRebateDataStore>(o => o.GetRebate(It.IsAny<string>()) == null);
        serviceCollection.AddTransient(_ => mockRebateDataStore);

        var mockProductDataStore = new Mock<IProductDataStore>();
        serviceCollection.AddTransient(_ => mockProductDataStore.Object);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var rebateService = serviceProvider.GetRequiredService<IRebateService>();
        
        // Act
        var result = rebateService.Calculate(new ()
        {
            ProductIdentifier = "product 1",
            RebateIdentifier = "rebate 1",
            Volume = 1
        });

        // Assert
        Assert.False(result.Success);
        mockProductDataStore.Verify(o => o.GetProduct(It.IsAny<string>()), Times.Never);
    }


    [Theory]
    [ClassData(typeof(FixedCashAmountCalculationServiceTestData))]
    public void CorrectlyCalculatesFixedCashAmountRebates(decimal rebateAmount, decimal expectedRebate)
    {
        // Arrange
        var serviceCollection = CompositionRoot.Prepare();

        var mockRebateDataStore = Mock.Of<IRebateDataStore>(o => o.GetRebate(It.IsAny<string>()) == new Rebate
        {
            Amount = rebateAmount, 
            Incentive = IncentiveType.FixedCashAmount
        });

        var mockProductDataStore = Mock.Of<IProductDataStore>(o => o.GetProduct(It.IsAny<string>()) == new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        });

        serviceCollection
            .AddTransient(_ => mockProductDataStore)
            .AddTransient(_ => mockRebateDataStore);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var rebateService = serviceProvider.GetRequiredService<IRebateService>();

        // Act
        var result = rebateService.Calculate(new()
        {
            ProductIdentifier = "product 1",
            RebateIdentifier = "rebate 1",
            Volume = 1
        });

        // Assert
        Assert.True(result.Success);
        Assert.Equal(expectedRebate, result.RebateAmount);
    }
}