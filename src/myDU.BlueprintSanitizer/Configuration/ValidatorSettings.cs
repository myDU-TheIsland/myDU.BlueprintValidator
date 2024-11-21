// <copyright file="ValidatorSettings.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Configuration
{
    using System;

    public class ValidatorSettings
    {
        public string[] ExternalDlls { get; set; } = Array.Empty<string>();

        public string[] DisableValidators { get; set; } = Array.Empty<string>();

        public string[] DisableSanitizers { get; set; } = Array.Empty<string>();

        public string QueueingURL { get; set; } = "http://queueing:9630";
    }
}
