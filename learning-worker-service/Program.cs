using learning_worker_service;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

IHost host = Host.CreateDefaultBuilder(args)
.UseWindowsService(options =>
{
    options.ServiceName = ".NET Joke Service";
})
    .ConfigureServices(services =>
    {
        LoggerProviderOptions.RegisterProviderOptions<
            EventLogSettings, EventLogLoggerProvider>(services);

        services.AddSingleton<JokeService>();
        services.AddHostedService<Worker>();
    })
    .ConfigureLogging((context, logging) =>
    {
        // See: https://github.com/dotnet/runtime/issues/47303
        logging.AddConfiguration(
            context.Configuration.GetSection("Logging"));
    })
    .Build();

await host.RunAsync();

