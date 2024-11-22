// <copyright file="DictionaryExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public static class DictionaryExtensions
    {
        public static SortedDictionary<TKey, TValue> Deserialize<TKey, TValue>(this BinaryReader reader)
            where TKey : struct, IDeserialize<TKey>
            where TValue : struct, IDeserialize<TValue>
        {
            int count = reader.ReadInt32();
            var dictionary = new SortedDictionary<TKey, TValue>();
            for (int i = 0; i < count; i++)
            {
                TKey key = SerializationExtensions.Deserialize<TKey>(reader);
                TValue value = SerializationExtensions.Deserialize<TValue>(reader);
                dictionary[key] = value;
            }

            return dictionary;
        }

        public static SortedDictionary<TKey, byte> Deserialize<TKey>(this BinaryReader reader)
            where TKey : struct, IDeserialize<TKey>
        {
            int count = reader.ReadInt32();
            var dictionary = new SortedDictionary<TKey, byte>();
            for (int i = 0; i < count; i++)
            {
                TKey key = SerializationExtensions.Deserialize<TKey>(reader);
                byte value = reader.ReadByte();
                dictionary[key] = value;
            }

            return dictionary;
        }
    }
}
