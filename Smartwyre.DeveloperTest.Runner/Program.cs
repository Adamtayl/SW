using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Runner;
using Smartwyre.DeveloperTest.Services.RebateServices;
using Smartwyre.DeveloperTest.Types;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        CompositionRoot.Prepare(services);
    })
    .Build();

CalculateRebate(host.Services);

await host.RunAsync();

static void CalculateRebate(IServiceProvider hostProvider)
{
    // This is very rough around the edges, with no input validation and a risky decimal.Parse
    Console.WriteLine("Rebate identifier:");
    var rebateIdentifier = Console.ReadLine();

    Console.WriteLine("Product identifier:");
    var productIdentifier = Console.ReadLine();

    Console.WriteLine("Volume:");
    var volume = decimal.Parse(Console.ReadLine()!);

    var calculationRequest = new CalculateRebateRequest()
    {
        ProductIdentifier = productIdentifier!,
        RebateIdentifier = rebateIdentifier!,
        Volume = volume
    };

    var rebateService = hostProvider.GetRequiredService<IRebateService>();

    // This always fails due to the Product returned from ProductDataStore not having 
    // any SupportedIncentives
    var calculationResult = rebateService.Calculate(calculationRequest);

    Console.WriteLine(calculationResult.Success
        ? $"Rebate calculated successfully with amount: {calculationResult.RebateAmount}"
        : "Rebate failed to calculate");


    Console.WriteLine("Press any key to exit.");
    Console.ReadKey();
    Environment.Exit(0);

}