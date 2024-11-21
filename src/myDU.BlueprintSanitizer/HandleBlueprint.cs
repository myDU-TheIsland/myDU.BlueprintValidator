// <copyright file="HandleBlueprint.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MyDU.BlueprintValidator.Sanitizers;
    using MyDU.BlueprintValidator.Validators;
    using Newtonsoft.Json;
    using NQ;

    public static class HandleBlueprint
    {
        public static List<ISanitize> Sanitizers { get; } = new List<ISanitize>();

        public static List<IValidate> Validators { get; } = new List<IValidate>();

        public static (bool IsGood, List<SanitizationResult> sanitizationResult, List<ValidationResult> validationResults) IsBlueprintGood(byte[] jsondata)
        {
            BlueprintData blueprint = JsonConvert.DeserializeObject<BlueprintData>(Encoding.UTF8.GetString(jsondata));

            List<SanitizationResult> sanitizationResult = new List<SanitizationResult>();
            List<ValidationResult> validationResults = new List<ValidationResult>();

            foreach (var sanitizer in Sanitizers)
            {
                sanitizationResult.Add(sanitizer.Santize(blueprint));
            }

            foreach (var validator in Validators)
            {
                validationResults.Add(validator.Validate(blueprint));
            }

            if (validationResults.Any(item => item.IsError) || sanitizationResult.Any(item => item.IsError))
            {
                return (false, sanitizationResult, validationResults);
            }

            return (true, sanitizationResult, validationResults);
        }
    }
}
