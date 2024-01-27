// See https://aka.ms/new-console-template for more information
using Common.MessageWriters;
using Gold.ConsoleMenu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using System.Runtime.CompilerServices;

var builder = CreateHostBuilder(args);
using IHost host = builder.Build();

Console.ForegroundColor = ConsoleColor.Gray;
MenuGenerator.Run();

// No need to call host.Run() or RunAsync() since all they do is start 
// hosted services or background services.  See MS Learn article ".NET Generic Host", 
// https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host, 
//      "When a host starts, it calls IHostedService.StartAsync on each implementation of
//      IHostedService registered in the service container's collection of hosted services.
//      In a worker service app, all IHostedService implementations that contain BackgroundService
//      instances have their BackgroundService.ExecuteAsync methods called."

static HostApplicationBuilder CreateHostBuilder(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    var services = builder.Services;
    services
        .AddScoped<IColorMessageWriter<ConsoleColor>, ConsoleMessageWriter>();

    return builder;

}
