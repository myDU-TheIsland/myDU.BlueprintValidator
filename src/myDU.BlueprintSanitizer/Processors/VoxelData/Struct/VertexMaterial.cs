// <copyright file="VertexMaterial.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct VertexMaterial : IDeserialize<VertexMaterial>
    {
        public byte Material;

        public VertexMaterial(byte material)
        {
            this.Material = material;
        }

        public static VertexMaterial Deserialize(Stream reader)
        {
            byte material = DeserializationExtensions.DeserializeByte(reader);
            return new VertexMaterial(material);
        }

        public static SortedDictionary<int, VertexMaterial> DeserializeSparse(int length, Stream reader)
        {
            var output = new SortedDictionary<int, VertexMaterial>();
            int i = 0;
            while (i < length)
            {
                var material = NullableExtensions.DeserializeNullableByte(reader);
                int more = DeserializationExtensions.DeserializeByte(reader) + 1;
                if (material.HasValue)
                {
                    output.Add(i, new VertexMaterial(material.Value));
                }

                i += more;
            }

            if (i != length)
            {
                throw new InvalidDataException("Bad data");
            }

            return output;
        }
    }
}
