using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateServices;

public interface IRebateService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
