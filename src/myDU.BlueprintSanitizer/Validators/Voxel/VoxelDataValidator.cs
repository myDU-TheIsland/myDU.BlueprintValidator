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
    using MyDU.BlueprintValidator.Processors.VoxelData.Struct;
    using Newtonsoft.Json.Linq;
    using static MyDU.BlueprintValidator.Validators.ValidationResult;

    internal class VoxelDataValidator : Validate
    {
        public VoxelDataValidator(IGameplayBank gameplayBank) : base(gameplayBank, nameof(VoxelDataValidator))
        {
        }

        public override Task<ValidationResult> ValidateAsync(BlueprintDataExtended blueprint)
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
                blueprint.Voxels.Add(this.HandleVoxelData(blueprint.VoxelData[x]));
            }

            return Task.FromResult(Succeeded());
        }

        private Voxel HandleVoxelData(JToken json)
        {
            BsonDocument bsonData = BsonSerializer.Deserialize<BsonDocument>(json.ToString());
            Voxel data = BsonSerializer.Deserialize<Voxel>(bsonData);

            foreach (var item in data.records)
            {
                try
                {
                    switch (item.Key)
                    {
                        case "meta":
                            item.Value.actualCellData = CompressionExtensions.Decompress<AggregateMetadata>(item.Value.data);
                            break;
                        case "voxel":
                            item.Value.actualCellData = CompressionExtensions.Decompress<VoxelCellData>(item.Value.data);
                            break;
                    }

                    continue;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            return data;
        }
    }
}
