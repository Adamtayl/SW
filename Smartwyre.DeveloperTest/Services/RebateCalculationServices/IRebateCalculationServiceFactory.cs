using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateCalculationServices;

public interface IRebateCalculationServiceFactory
{
    public IRebateCalculationService Resolve(IncentiveType incentiveType);
}