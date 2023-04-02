using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Composition;

namespace Smartwyre.DeveloperTest.Runner;
public class CompositionRoot
{
    public static IServiceCollection Prepare(IServiceCollection? services = null)
    {
        services ??= new ServiceCollection();

        services.AddRebateServiceDescriptors();

        return services;
    }
}
