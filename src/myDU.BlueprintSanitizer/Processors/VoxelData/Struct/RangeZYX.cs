// <copyright file="RangeZYX.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Drawing;
    using System.IO;
    using System.Numerics;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct RangeZYX : IDeserialize<RangeZYX>
    {
        public Vector3 Origin { get; set; }

        public Vector3 Size { get; set; }

        public ulong Volume()
        {
            return (ulong)(this.Size.X * this.Size.Y * this.Size.Z);
        }

        public void Deserialize(BinaryReader reader)
        {
            this.Origin = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            this.Size = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }
    }
}
