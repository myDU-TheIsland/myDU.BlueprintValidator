// <copyright file="FixedPoint.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct FixedPoint : IDeserialize<FixedPoint>
    {
        public ulong Value { get; set; }

        public FixedPoint(double value)
        {
            this.Value = (ulong)(value / BitConverter.Int64BitsToDouble(0x3E70000000000000));
        }

        public static FixedPoint Deserialize(Stream reader)
        {
            return new FixedPoint(DeserializationExtensions.DeserializeUInt64(reader));
        }

        public static FixedPoint FromDouble(double value)
        {
            return new FixedPoint((ulong)(value / BitConverter.Int64BitsToDouble(0x3E70000000000000)));
        }

        public double ToDouble()
        {
            return this.Value * BitConverter.Int64BitsToDouble(0x3E70000000000000);
        }

        public override string ToString()
        {
            return this.ToDouble().ToString();
        }
    }
}
