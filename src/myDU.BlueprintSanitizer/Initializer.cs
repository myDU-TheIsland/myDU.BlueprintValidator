// <copyright file="Initializer.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Backend;
    using Microsoft.Extensions.Configuration;
    using MyDU.BlueprintValidator.Configuration;
    using MyDU.BlueprintValidator.Sanitizers.Blueprint;
    using MyDU.BlueprintValidator.Sanitizers.Element;
    using MyDU.BlueprintValidator.Validators.Blueprint;
    using MyDU.BlueprintValidator.Validators.Voxel;

    public static class Initializer
    {
        /// <summary>
        /// Gets or sets the validator settings.
        /// </summary>
        /// <value>The setup settings.</value>
        public static ValidatorSettings Settings { get; set; } = new ValidatorSettings();

        /// <summary>
        /// Gets or sets the configuration builder.
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        public static void Initialize(IGameplayBank gameplayBank)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            ValidatorSettings settings = new ValidatorSettings();
            Configuration.GetSection("Settings").Bind(settings);
            Settings = settings;

            HandleBlueprint.Sanitizers.Add(new BlueprintSanitizer(gameplayBank));
            HandleBlueprint.Sanitizers.Add(new ElementSanitizer(gameplayBank));
            HandleBlueprint.Sanitizers.Add(new ElementAttributeSanitizer(gameplayBank));

            HandleBlueprint.Validators.Add(new BlueprintValidator(gameplayBank));
            HandleBlueprint.Validators.Add(new VoxelDataValidator(gameplayBank));
            HandleBlueprint.Validators.Add(new VoxelOutOfBoundsValidator(gameplayBank));

            foreach (var dlls in Settings.ExternalDlls)
            {
                Type[] types = Assembly.Load(dlls).GetTypes()
                    .Where(type => typeof(IInitializer).IsAssignableFrom(type) && !type.IsInterface)
                    .ToArray();

                foreach (var typeDefinition in types)
                {
                    if (typeDefinition.BaseType.GetGenericTypeDefinition() != typeof(IInitializer))
                    {
                        continue;
                    }

                    var init = (IInitializer)Activator.CreateInstance(typeDefinition);
                    init.Initialize();
                }
            }
        }
    }
}
