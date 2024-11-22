// <copyright file="NullableExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public static class NullableExtensions
    {
        public static TValue? Deserialize<TValue>(this BinaryReader reader)
            where TValue : struct, IDeserialize<TValue>
        {
            if (reader.ReadBoolean())
            {
                return SerializationExtensions.Deserialize<TValue>(reader);
            }
            else
            {
                return null;
            }
        }
    }
}
