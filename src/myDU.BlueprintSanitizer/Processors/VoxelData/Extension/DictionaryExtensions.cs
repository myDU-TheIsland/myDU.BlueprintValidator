// <copyright file="DictionaryExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;
    using MyDU.BlueprintValidator.Processors.VoxelData.Struct;

    public static class DictionaryExtensions
    {
        public static Dictionary<RangeZYX, VertexMaterial> Deserialize(Stream reader)
        {
            int count = DeserializationExtensions.DeserializeInt32(reader);
            var dictionary = new Dictionary<RangeZYX, VertexMaterial>();
            for (int i = 0; i < count; i++)
            {
                RangeZYX key = RangeZYX.Deserialize(reader);
                VertexMaterial value = VertexMaterial.Deserialize(reader);
                dictionary[key] = value;
            }

            return dictionary;
        }
    }

    public static class SortedDictionaryExtensions
    {
        public static SortedDictionary<MaterialId, FixedPoint> Deserialize(Stream reader)
        {
            int count = DeserializationExtensions.DeserializeInt32(reader);
            var dictionary = new SortedDictionary<MaterialId, FixedPoint>();
            for (int i = 0; i < count; i++)
            {
                MaterialId key = MaterialId.Deserialize(reader);
                FixedPoint value = FixedPoint.Deserialize(reader);

                dictionary[key] = value;
            }

            return dictionary;
        }

        public static SortedDictionary<TKey, byte> Deserialize<TKey>(this Stream reader)
                    where TKey : struct, IDeserialize<TKey>
        {
            int count = DeserializationExtensions.DeserializeInt32(reader);
            var dictionary = new SortedDictionary<TKey, byte>();
            for (int i = 0; i < count; i++)
            {
                TKey key = TKey.Deserialize(reader);
                byte value = DeserializationExtensions.DeserializeByte(reader);
                dictionary[key] = value;
            }

            return dictionary;
        }
    }
}