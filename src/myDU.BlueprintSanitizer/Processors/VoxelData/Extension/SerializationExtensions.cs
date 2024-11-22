// <copyright file="SerializationExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;
    using Newtonsoft.Json.Linq;

    public static class SerializationExtensions
    {
        public static TValue Deserialize<TValue>(this BinaryReader reader) where TValue : IDeserialize<TValue>, new()
        {
            TValue result = new TValue();
            result.Deserialize(reader);
            return result;
        }

        public static void Serialize(this uint value, BinaryWriter writer)
        {
            writer.Write(BitConverter.GetBytes(value));
        }

        public static uint DeserializeUInt32(this BinaryReader reader)
        {
            return BitConverter.ToUInt32(reader.ReadBytes(4), 0);
        }

        public static void Serialize(this int value, BinaryWriter writer)
        {
            writer.Write(BitConverter.GetBytes(value));
        }

        public static int DeserializeInt32(this BinaryReader reader)
        {
            return BitConverter.ToInt32(reader.ReadBytes(4), 0);
        }

        public static void Serialize(this ulong value, BinaryWriter writer)
        {
            writer.Write(BitConverter.GetBytes(value));
        }

        public static ulong DeserializeUInt64(this BinaryReader reader)
        {
            return BitConverter.ToUInt64(reader.ReadBytes(8), 0);
        }

        public static void Serialize(this long value, BinaryWriter writer)
        {
            writer.Write(BitConverter.GetBytes(value));
        }

        public static long DeserializeInt64(this BinaryReader reader)
        {
            return BitConverter.ToInt64(reader.ReadBytes(8), 0);
        }

        public static void Serialize(this double value, BinaryWriter writer)
        {
            writer.Write(BitConverter.GetBytes(value));
        }

        public static double DeserializeDouble(this BinaryReader reader)
        {
            return BitConverter.ToDouble(reader.ReadBytes(8), 0);
        }
    }
}
