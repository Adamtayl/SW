using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculationServices;

public class FixedRateRebateCalculationService : IRebateCalculationService
{
    public CalculateRebateResult CalculateRebate(Product? product, Rebate rebate, CalculateRebateRequest request)
    {
        if (product == null)
        {
            return new(false);
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        {
            return new(false);
        }
        else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        {
            return new(false);
        }
        else
        {
            return new(true, product.Price * rebate.Percentage * request.Volume);
        }
    }
}