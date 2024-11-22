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
        public RangeZYX? BoundingBox { get; set; }

        public SortedDictionary<MaterialId, FixedPoint> MaterialStats { get; set; }

        public Inertia? Inertia { get; set; }

        public ulong ServerTimestamp { get; set; }

        public ulong ServerPreviousVersion { get; set; }

        public static HeavyMetadata Deserialize(Stream reader)
        {
            RangeZYX? boundingBox = NullableExtensions.DeserializeNullable<RangeZYX>(reader);
            SortedDictionary<MaterialId, FixedPoint> materialStats = NullableExtensions.DeserializeNullableSortedDictionary<MaterialId, FixedPoint>(reader);
            Inertia? inertia = NullableExtensions.DeserializeNullable<Inertia>(reader);
            ulong serverTimestamp = DeserializationExtensions.DeserializeUInt64(reader);
            ulong serverPreviousVersion = DeserializationExtensions.DeserializeUInt64(reader);
            return new HeavyMetadata
            {
                BoundingBox = boundingBox,
                MaterialStats = materialStats,
                Inertia = inertia,
                ServerTimestamp = serverTimestamp,
                ServerPreviousVersion = serverPreviousVersion,
            };
        }
    }
}
