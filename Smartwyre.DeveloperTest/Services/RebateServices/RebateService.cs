using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.RebateCalculationServices;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.RebateServices;

// The implementations of this interface could do with a bit of a tidy
// But I've left as is for now due to time constraints
public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;

    private readonly IRebateCalculationServiceFactory _rebateCalculationServiceFactory;

    public RebateService(
        IRebateDataStore rebateDataStore,
        IProductDataStore productDataStore,
        IRebateCalculationServiceFactory rebateCalculationServiceFactory
    )
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculationServiceFactory = rebateCalculationServiceFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);

        if (rebate is null)
        {
            return new(false);
        }

        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        var calculationService = _rebateCalculationServiceFactory.Resolve(rebate.Incentive);

        var result = calculationService.CalculateRebate(product, rebate, request);

        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, result.RebateAmount);
        }

        return result;
    }
}
