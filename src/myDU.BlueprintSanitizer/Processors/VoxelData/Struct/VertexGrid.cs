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

        public SortedDictionary<int, VertexMaterial> SparseMaterials { get; set; } = new SortedDictionary<int, VertexMaterial>();

        public SortedDictionary<int, VertexVoxel> SparseVertices { get; set; } = new SortedDictionary<int, VertexVoxel>();

        public const uint MAGIC = 0xe881339e;
        public const uint VERSION = 9;

        public VertexGrid()
        {
        }

        public VertexGrid(RangeZYX range, RangeZYX innerRange)
        {
            this.Range = range;
            this.InnerRange = innerRange;
            this.SparseMaterials = new SortedDictionary<int, VertexMaterial>();
            this.SparseVertices = new SortedDictionary<int, VertexVoxel>();
        }

        public static VertexGrid Deserialize(Stream reader)
        {
            uint magic = DeserializationExtensions.DeserializeUInt32(reader);
            Assertions.AssertMagic(magic, MAGIC);
            uint version = DeserializationExtensions.DeserializeUInt32(reader);
            Assertions.AssertVersion(version, VERSION);
            RangeZYX range = RangeZYX.Deserialize(reader);
            RangeZYX innerRange = RangeZYX.Deserialize(reader);
            int length = (int)range.Volume();
            SortedDictionary<int, VertexMaterial> sparseMaterials = VertexMaterial.DeserializeSparse(length, reader);
            SortedDictionary<int, VertexVoxel> sparseVertices = VertexVoxel.DeserializeSparse(length, reader);

            return new VertexGrid(range, innerRange)
            {
                SparseMaterials = sparseMaterials,
                SparseVertices = sparseVertices,
            };
        }
    }
}
