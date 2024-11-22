// <copyright file="DeserializationExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System;
    using System.IO;

    public static class DeserializationExtensions
    {
        public static uint DeserializeUInt32(this Stream reader)
        {
            var buffer = new byte[sizeof(uint)];
            reader.ReadExactly(buffer, 0, buffer.Length);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static ulong DeserializeUInt64(this Stream reader)
        {
            var buffer = new byte[sizeof(ulong)];
            reader.ReadExactly(buffer, 0, buffer.Length);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static int DeserializeInt32(this Stream reader)
        {
            var buffer = new byte[sizeof(int)];
            reader.ReadExactly(buffer, 0, buffer.Length);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static long DeserializeInt64(this Stream reader)
        {
            var buffer = new byte[sizeof(long)];
            reader.ReadExactly(buffer, 0, buffer.Length);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static float DeserializeFloat(this Stream reader)
        {
            var buffer = new byte[sizeof(float)];
            reader.ReadExactly(buffer, 0, buffer.Length);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static double DeserializeDouble(this Stream reader)
        {
            var buffer = new byte[sizeof(double)];
            reader.ReadExactly(buffer, 0, buffer.Length);
            return BitConverter.ToDouble(buffer, 0);
        }

        public static byte DeserializeByte(this Stream reader)
        {
            return (byte)reader.ReadByte();
        }
    }
}
