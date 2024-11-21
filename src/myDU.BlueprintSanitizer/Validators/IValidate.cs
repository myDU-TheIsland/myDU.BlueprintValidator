// <copyright file="IValidate.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Validators
{
    using System.Threading.Tasks;
    using NQ;

    public interface IValidate
    {
        string Name { get; }

        Task<ValidationResult> ValidateAsync(BlueprintData blueprint);

        ValidationResult Validate(BlueprintData blueprint) => this.ValidateAsync(blueprint).GetAwaiter().GetResult();
    }
}
