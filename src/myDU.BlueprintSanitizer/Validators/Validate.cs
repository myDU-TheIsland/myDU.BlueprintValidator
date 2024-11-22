// <copyright file="Validate.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Validators
{
    using System.Threading.Tasks;
    using Backend;
    using MyDU.BlueprintValidator.Classes;
    using NQ;

    public abstract class Validate : IValidate
    {
        protected IGameplayBank GameplayBank { get; }

        public string Name { get; }

        public Validate(IGameplayBank gameplayBank, string name)
        {
            this.GameplayBank = gameplayBank;
            this.Name = name;
        }

        public abstract Task<ValidationResult> ValidateAsync(BlueprintDataExtended blueprint);
    }
}
