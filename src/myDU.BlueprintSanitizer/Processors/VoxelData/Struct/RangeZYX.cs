// <copyright file="RangeZYX.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.IO;
    using System.Numerics;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct RangeZYX : IDeserialize<RangeZYX>
    {
        public Vector3 Origin;
        public Vector3 Size;

        public RangeZYX(Vector3 origin, Vector3 extents)
        {
            this.Origin = origin;
            this.Size = extents;
        }

        public static RangeZYX Deserialize(Stream reader)
        {
            var origin = Vector3Extensions.Deserialize(reader);
            var size = Vector3Extensions.Deserialize(reader);
            return new RangeZYX(origin, size);
        }

        public ulong Volume()
        {
            return (ulong)(this.Size.X * this.Size.Y * this.Size.Z);
        }
    }
}
