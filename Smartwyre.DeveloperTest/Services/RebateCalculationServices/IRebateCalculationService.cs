using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculationServices;

public interface IRebateCalculationService
{
    public CalculateRebateResult CalculateRebate(Product? product, Rebate rebate, CalculateRebateRequest request);
}