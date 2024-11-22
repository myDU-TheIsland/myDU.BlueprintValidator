// <copyright file="FixedPoint.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct FixedPoint : IDeserialize<FixedPoint>
    {
        public ulong Value { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            this.Value = reader.ReadUInt64();
        }
    }
}
