// <copyright file="LightMetadata.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct LightMetadata : IDeserialize<LightMetadata>
    {
        public bool? Vox { get; set; }

        public bool? Mod { get; set; }

        public long? HashVoxel { get; set; }

        public long? HashDecor { get; set; }

        public double? Entropy { get; set; }

        public static LightMetadata Deserialize(Stream reader)
        {
            byte value = DeserializationExtensions.DeserializeByte(reader);
            bool? vox = BoolExtensions.ToBool((byte)((value >> 2) & 3));
            bool? mod = BoolExtensions.ToBool((byte)(value & 3));

            long? hashVoxel = NullableExtensions.DeserializeNullableInt64(reader);
            long? hashDecor = NullableExtensions.DeserializeNullableInt64(reader);
            double? entropy = NullableExtensions.DeserializeNullableDouble(reader);

            return new LightMetadata
            {
                Vox = vox,
                Mod = mod,
                HashVoxel = hashVoxel,
                HashDecor = hashDecor,
                Entropy = entropy,
            };
        }
    }
}
