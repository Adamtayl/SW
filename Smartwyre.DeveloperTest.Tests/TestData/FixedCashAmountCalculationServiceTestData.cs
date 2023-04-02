using Xunit;

namespace Smartwyre.DeveloperTest.Tests.TestData;

public class FixedCashAmountCalculationServiceTestData : TheoryData<decimal, decimal>
{
    public FixedCashAmountCalculationServiceTestData()
    {
        Add(1M, 1M);
        Add(-1M, -1M);
        Add(decimal.MaxValue, decimal.MaxValue);
    }
}