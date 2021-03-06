﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rhisis.Core.DependencyInjection;
using Rhisis.Core.Extensions;
using Rhisis.Core.Structures.Configuration;
using Rhisis.Core.Structures.Configuration.World;
using Rhisis.Database;
using Rhisis.Game;
using Rhisis.Game.Abstractions.Protocol;
using Rhisis.Redis;
using Rhisis.WorldServer.CoreClient;
using Sylver.HandlerInvoker;
using Sylver.Network.Data;
using System.Threading.Tasks;

namespace Rhisis.WorldServer
{
    public static class Program
    {
        private static async Task Main()
        {
            const string culture = "en-US";

            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(EnvironmentExtension.GetCurrentEnvironementDirectory());
                    configApp.AddJsonFile(ConfigurationConstants.WorldServerPath, optional: false);
                    configApp.AddJsonFile(ConfigurationConstants.DatabasePath, optional: false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddMemoryCache();
                    services.Configure<WorldConfiguration>(hostContext.Configuration.GetSection(ConfigurationConstants.WorldServer));
                    services.Configure<CoreConfiguration>(hostContext.Configuration.GetSection(ConfigurationConstants.CoreServer));
                    
                    services.AddDatabase(hostContext.Configuration);
                    services.AddHandlers();
                    services.AddInjectableServices();
                    services.AddGameSystems();

                    // World server configuration
                    services.AddSingleton<IWorldServer, WorldServer>();
                    services.AddSingleton<IHostedService, WorldServerService>();
                    
                    // Core server client configuration
                    services.AddSingleton<IHostedService, WorldCoreClient>();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .UseRedis((host, options) =>
                {
                    options.Host = "rhisis.redis";
                })
                .UseConsoleLifetime()
                .SetConsoleCulture(culture)
                .Build();

            await host
                .AddHandlerParameterTransformer<INetPacketStream, IPacketDeserializer>((source, dest) =>
                {
                    dest?.Deserialize(source);
                    return dest;
                })
                .RunAsync();
        }
    }
}