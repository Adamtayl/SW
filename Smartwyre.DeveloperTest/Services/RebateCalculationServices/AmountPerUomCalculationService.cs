using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculationServices;

public class AmountPerUomCalculationService : IRebateCalculationService
{
    public CalculateRebateResult CalculateRebate(Product? product, Rebate rebate, CalculateRebateRequest request)
    {
        if (product == null)
        {
            return new(false);
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            return new(false);
        }
        else if (rebate.Amount == 0 || request.Volume == 0)
        {
            return new(false);
        }
        else
        {
            return new(true, rebate.Amount * request.Volume);
        }
    }
}