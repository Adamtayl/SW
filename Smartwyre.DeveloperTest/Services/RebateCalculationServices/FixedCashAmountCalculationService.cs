using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculationServices;

public class FixedCashAmountCalculationService : IRebateCalculationService
{
    public CalculateRebateResult CalculateRebate(Product? product, Rebate rebate, CalculateRebateRequest request)
    {
        if (product == null)
        {
            return new(false);
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            return new(false);
        }
        else if (rebate.Amount == 0)
        {
            return new(false);
        }
        else
        {
            return new(true, rebate.Amount);
        }
    }
}