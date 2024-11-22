// <copyright file="Vector3Extensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System;
    using System.IO;
    using System.Numerics;

    public static class Vector3Extensions
    {
        public static void Serialize(this Vector3 vector, Stream writer)
        {
            vector.X.Serialize(writer);
            vector.Y.Serialize(writer);
            vector.Z.Serialize(writer);
        }

        public static Vector3 Deserialize(Stream reader)
        {
            var x = reader.DeserializeInt32();
            var y = reader.DeserializeInt32();
            var z = reader.DeserializeInt32();
            return new Vector3(x, y, z);
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
        }
    }
}
