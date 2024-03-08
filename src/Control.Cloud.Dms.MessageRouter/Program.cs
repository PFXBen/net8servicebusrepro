using Control.Cloud.Dms.MessageRouter.Externals;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices(s =>
        {
            s.AddApplicationInsightsTelemetryWorkerService();
            s.ConfigureFunctionsApplicationInsights();
            s.AddSingleton<ITelemetryInitializer>(provider =>
                new RoleNameTelemetryInitializer("control-cloud-dms-messagerouter"));
            s.AddOptions<DmsInputQueueClientOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("DmsInputQueueClientOptions").Bind(settings);
            });
            s.AddOptions<DmsMessageQueueClientOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("DmsMessageQueueClientOptions").Bind(settings);
            });
            s.AddAzureClients(clientBuilder =>
            {
                clientBuilder
                    .AddServiceBusClient(
                        Environment.GetEnvironmentVariable("DmsInputQueueClientOptions:ServiceBusConnectionString"))
                    .WithName("DmsInputSb");
                clientBuilder
                    .AddServiceBusClient(
                        Environment.GetEnvironmentVariable("DmsMessageQueueClientOptions:ServiceBusConnectionString"))
                    .WithName("DmsMessageSb");
            });
        })
        .Build();

host.Run();
