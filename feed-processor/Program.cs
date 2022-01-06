using feed_processor.Interfaces;
using feed_processor.Services;
using feed_processor.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();


using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Akka .NET Feed Processor";
    }).UseSerilog()    
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;

        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<ILogger>(Log.Logger);
        services.AddSingleton<IMainService, MainService>();
        services.AddHostedService<worker>();
    })
    .Build();



await host.RunAsync();