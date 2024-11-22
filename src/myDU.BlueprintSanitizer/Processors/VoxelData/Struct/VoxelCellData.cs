// <copyright file="VoxelCellData.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct VoxelCellData : IDeserialize<VoxelCellData>
    {
        public VertexGrid Grid { get; set; } = new VertexGrid();

        public MaterialMapper Mapping { get; set; } = new MaterialMapper();

        public byte IsDiff { get; set; } = 1;

        public const uint MAGIC = 0x27b8a013;
        public const uint VERSION = 6;

        public VoxelCellData()
        {
        }

        public void Deserialize(BinaryReader reader)
        {
            uint magic = reader.ReadUInt32();
            Assertions.AssertMagic(magic, MAGIC);
            uint version = reader.ReadUInt32();
            Assertions.AssertVersion(version, VERSION);
            this.Grid.Deserialize(reader);
            this.Mapping.Deserialize(reader);
            this.IsDiff = reader.ReadByte();
        }
    }
}
