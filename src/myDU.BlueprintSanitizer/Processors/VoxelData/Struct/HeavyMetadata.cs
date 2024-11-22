// <copyright file="HeavyMetadata.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct HeavyMetadata : IDeserialize<HeavyMetadata>
    {
        public RangeZYX BoundingBox { get; set; }

        public SortedDictionary<MaterialId, FixedPoint> MaterialStats { get; set; }

        public Inertia? Inertia { get; set; }

        public ulong ServerTimestamp { get; set; }

        public ulong ServerPreviousVersion { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            this.BoundingBox = SerializationExtensions.Deserialize<RangeZYX>(reader);
            this.MaterialStats = DictionaryExtensions.Deserialize<MaterialId, FixedPoint>(reader);
            this.Inertia = NullableExtensions.Deserialize<Inertia>(reader);
            this.ServerTimestamp = reader.ReadUInt64();
            this.ServerPreviousVersion = reader.ReadUInt64();
        }
    }
}
