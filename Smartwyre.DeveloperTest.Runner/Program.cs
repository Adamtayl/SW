using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Runner;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        CompositionRoot.Prepare(services);
    })
    .Build();

await host.RunAsync();


