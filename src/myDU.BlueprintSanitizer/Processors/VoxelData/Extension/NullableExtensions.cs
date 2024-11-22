// <copyright file="NullableExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public static class NullableExtensions
    {
        public static TValue? DeserializeNullable<TValue>(this Stream reader)
            where TValue : struct, IDeserialize<TValue>
        {
            if (reader.ReadByte() == 1)
            {
                return TValue.Deserialize(reader);
            }
            else
            {
                return null;
            }
        }

        public static SortedDictionary<TKey, TValue> DeserializeNullableSortedDictionary<TKey, TValue>(this Stream reader)
            where TKey : struct, IDeserialize<TKey>
            where TValue : struct, IDeserialize<TValue>
        {
            if (reader.ReadByte() == 1)
            {
                int count = DeserializationExtensions.DeserializeInt32(reader);
                var dictionary = new SortedDictionary<TKey, TValue>();
                for (int i = 0; i < count; i++)
                {
                    TKey key = TKey.Deserialize(reader);
                    TValue value = TValue.Deserialize(reader);
                    dictionary[key] = value;
                }

                return dictionary;
            }
            else
            {
                return null;
            }
        }

        public static byte? DeserializeNullableByte(Stream reader)
        {
            byte b = (byte)reader.ReadByte();
            return b == 0 ? (byte?)null : b;
        }

        public static long? DeserializeNullableInt64(Stream reader)
        {
            bool hasValue = reader.ReadByte() != 0;
            return hasValue ? (long?)DeserializationExtensions.DeserializeInt64(reader) : null;
        }

        public static double? DeserializeNullableDouble(Stream reader)
        {
            bool hasValue = reader.ReadByte() != 0;
            return hasValue ? (double?)DeserializationExtensions.DeserializeDouble(reader) : null;
        }
    }
}
