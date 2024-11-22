// <copyright file="VertexGrid.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct VertexGrid : IDeserialize<VertexGrid>
    {
        public RangeZYX Range { get; set; } = new RangeZYX();

        public RangeZYX InnerRange { get; set; } = new RangeZYX();

        public Dictionary<Range, VertexMaterial> SparseMaterials { get; set; } = new Dictionary<Range, VertexMaterial>();

        public Dictionary<Range, VertexVoxel> SparseVertices { get; set; } = new Dictionary<Range, VertexVoxel>();

        public const uint MAGIC = 0xe881339e;
        public const uint VERSION = 9;

        public VertexGrid()
        {
        }

        public void Deserialize(BinaryReader reader)
        {
            uint magic = reader.ReadUInt32();
            Assertions.AssertMagic(magic, MAGIC);
            uint version = reader.ReadUInt32();
            Assertions.AssertVersion(version, VERSION);
            this.Range.Deserialize(reader);
            this.InnerRange.Deserialize(reader);
            int length = (int)this.Range.Volume();
            this.SparseMaterials = VertexMaterial.DeserializeSparse(length, reader);
            this.SparseVertices = VertexVoxel.DeserializeSparse(length, reader);
        }
    }
}
