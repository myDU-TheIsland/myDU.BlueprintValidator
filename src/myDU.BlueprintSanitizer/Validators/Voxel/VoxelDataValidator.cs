// <copyright file="VoxelDataValidator.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Validators.Voxel
{
    using System;
    using System.Threading.Tasks;
    using Backend;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MyDU.BlueprintValidator.Classes;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using Newtonsoft.Json.Linq;
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

            if (blueprint.VoxelData == null)
            {
                return Task.FromResult(Failed("blueprint has no voxel data"));
            }

            if (blueprint.VoxelData.Count == 0)
            {
                //no voxel data
                return Task.FromResult(Succeeded());
            }

            for (var x = 0; x < blueprint.VoxelData.Count; x++)
            {
                blueprint.VoxelData[x] = this.HandleVoxelData(blueprint.VoxelData[x]);
            }

            return Task.FromResult(Succeeded());
        }

        private JToken HandleVoxelData(JToken json)
        {
            BsonDocument bsonData = BsonSerializer.Deserialize<BsonDocument>(json.ToString());
            Voxel data = BsonSerializer.Deserialize<Voxel>(bsonData);

            foreach (var item in data.records)
            {
                try
                {
                    item.Value.actualCellData = CompressionExtensions.Decompress(item.Value.data);
                    continue;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            return JToken.Parse(data.ToJson());
        }
    }
}
