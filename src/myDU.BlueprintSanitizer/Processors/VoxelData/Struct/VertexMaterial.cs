// <copyright file="VertexMaterial.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyDU.BlueprintValidator.Processors.VoxelData.Enum;
    using MyDU.BlueprintValidator.Processors.VoxelData.Exception;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct VertexMaterial : IDeserialize<VertexMaterial>
    {
        public byte Material { get; set; }

        public VertexMaterial(byte material)
        {
            this.Material = material;
        }

        public static Dictionary<Range, VertexMaterial> DeserializeSparse(int length, BinaryReader reader)
        {
            var output = new Dictionary<Range, VertexMaterial>();
            int i = 0;
            while (i < length)
            {
                var material = reader.ReadByte();
                int more = reader.ReadByte() + 1;
                if (material != 0)
                {
                    output[new Range(i, i + more)] = new VertexMaterial(material);
                }

                i += more;
            }

            if (i != length)
            {
                throw new DeserializationException(DeserializeError.BadData);
            }

            return output;
        }

        public void Deserialize(BinaryReader reader)
        {
            this.Material = reader.ReadByte();
        }
    }
}
