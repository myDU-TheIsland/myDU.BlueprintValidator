// <copyright file="Program.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.CLI
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend;
    using Backend.Business;
    using BotLib.BotClient;
    using BotLib.Protocols;
    using BotLib.Protocols.Queuing;
    using Microsoft.Extensions.DependencyInjection;
    using MyDU.BlueprintValidator;
    using NQ.Router;
    using NQutils;
    using NQutils.Sql;
    using Orleans;

    public static class Program
    {
        public static IDuClientFactory RestDuClientFactory => ServiceProvider.GetRequiredService<IDuClientFactory>();

        public static IServiceProvider ServiceProvider { get; private set; }

        public static IClusterClient Orleans { get; private set; }

        public static IDataAccessor DataAccessor { get; private set; }

        public static Client Bot { get; }

        public static async Task Main(string[] args)
        {
            NQutils.Config.Config.ReadYamlFile("mod", "./dual.yaml");
            await Setup().ConfigureAwait(false);

            Initializer.Initialize(gameplayBank: ServiceProvider.GetRequiredService<IGameplayBank>());
            var bytes = await File.ReadAllBytesAsync("TestBP.json").ConfigureAwait(false);
            var result = HandleBlueprint.IsBlueprintGood(bytes);

            if (result.IsGood)
            {
                Console.WriteLine("Good BP");
            }
            else
            {
                Console.WriteLine("Bad BP");
                foreach (var sanitization in result.sanitizationResult)
                {
                    if (sanitization.Success)
                    {
                        continue;
                    }

                    Console.WriteLine(@$"Sanitization Message: {sanitization.Message}");
                }

                foreach (var validation in result.validationResults)
                {
                    if (validation.Success)
                    {
                        continue;
                    }

                    Console.WriteLine(@$"Validation Message: {validation.Message}");
                }
            }

            return;
        }

        /// <summary>
        /// Configure the App
        /// </summary>
        /// <returns></returns>
        public static async Task Setup()
        {
            var services = new ServiceCollection();

            //services.RegisterCoreServices();
            var qurl = Environment.GetEnvironmentVariable("QUEUEING");

            if (string.IsNullOrEmpty(qurl))
            {
                qurl = "http://queueing:9630";
            }

            services
            .AddSingleton<ISql, Sql>()
            .AddInitializableSingleton<IGameplayBank, GameplayBank>()
            .AddSingleton<ILocalizationManager, LocalizationManager>()
            .AddTransient<IDataAccessor, DataAccessor>()
            .AddOrleansClient("IntegrationTests")
            .AddHttpClient()
            .AddTransient<NQutils.Stats.IStats, NQutils.Stats.FakeIStats>()
            .AddSingleton<IQueuing, RealQueuing>(sp => new RealQueuing(qurl, sp.GetRequiredService<IHttpClientFactory>().CreateClient()))
            .AddSingleton<IDuClientFactory, BotLib.Protocols.GrpcClient.DuClientFactory>();

            var sp = services.BuildServiceProvider();
            ServiceProvider = sp;
            await ServiceProvider.StartServices().ConfigureAwait(false);
            ClientExtensions.SetSingletons(sp);
            ClientExtensions.UseFactory(sp.GetRequiredService<IDuClientFactory>());
            Orleans = ServiceProvider.GetRequiredService<IClusterClient>();
            DataAccessor = ServiceProvider.GetRequiredService<IDataAccessor>();
        }
    }
}
