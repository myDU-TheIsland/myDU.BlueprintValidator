// <copyright file="SerializationExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System;
    using System.IO;
    using System.Numerics;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public static class SerializationExtensions
    {
        public static void Serialize(this uint value, Stream writer)
        {
            writer.Write(BitConverter.GetBytes(value), 0, sizeof(uint));
        }

        public static void Serialize(this ulong value, Stream writer)
        {
            writer.Write(BitConverter.GetBytes(value), 0, sizeof(ulong));
        }

        public static void Serialize(this int value, Stream writer)
        {
            writer.Write(BitConverter.GetBytes(value), 0, sizeof(int));
        }

        public static void Serialize(this long value, Stream writer)
        {
            writer.Write(BitConverter.GetBytes(value), 0, sizeof(long));
        }

        public static void Serialize(this float value, Stream writer)
        {
            writer.Write(BitConverter.GetBytes(value), 0, sizeof(float));
        }

        public static void Serialize(this double value, Stream writer)
        {
            writer.Write(BitConverter.GetBytes(value), 0, sizeof(double));
        }

        public static void Serialize(this byte value, Stream writer)
        {
            writer.WriteByte(value);
        }
    }
}
