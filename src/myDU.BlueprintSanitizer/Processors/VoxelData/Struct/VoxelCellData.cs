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

        public VoxelCellData(VertexGrid grid, MaterialMapper mapping)
        {
            this.Grid = grid;
            this.Mapping = mapping;
            this.IsDiff = 1;
        }

        public static VoxelCellData Deserialize(Stream reader)
        {
            uint magic = DeserializationExtensions.DeserializeUInt32(reader);
            Assertions.AssertMagic(magic, MAGIC);
            uint version = DeserializationExtensions.DeserializeUInt32(reader);
            Assertions.AssertVersion(version, VERSION);
            VertexGrid grid = VertexGrid.Deserialize(reader);
            MaterialMapper mapping = MaterialMapper.Deserialize(reader);
            byte isDiff = DeserializationExtensions.DeserializeByte(reader);

            return new VoxelCellData(grid, mapping)
            {
                IsDiff = isDiff,
            };
        }
    }
}
