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

        public bool? R { get; set; }

        public long? HashVoxel { get; set; }

        public long? HashDecor { get; set; }

        public float? Entropy { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            byte value = reader.ReadByte();
            this.Vox = BoolExtensions.IntToMaybeBool((byte)((value >> 2) & 3));
            this.R = BoolExtensions.IntToMaybeBool((byte)(value & 3));
        }
    }
}
