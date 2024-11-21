// <copyright file="VoxelDataValidator.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Validators.Voxel
{
    using System.Threading.Tasks;
    using Backend;
    using NQ;
    using static MyDU.BlueprintValidator.Validators.ValidationResult;

    internal class VoxelDataValidator : Validate
    {
        public VoxelDataValidator(IGameplayBank gameplayBank) : base(gameplayBank, nameof(VoxelDataValidator))
        {
        }

        public override Task<ValidationResult> ValidateAsync(BlueprintData blueprint)
        {
            if (blueprint == null)
            {
                return Task.FromResult(Failed("Blueprint is null"));
            }

            return Task.FromResult(Succeeded());
        }
    }
}
