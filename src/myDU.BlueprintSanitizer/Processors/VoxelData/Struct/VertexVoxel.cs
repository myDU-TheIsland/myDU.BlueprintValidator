// <copyright file="VertexVoxel.cs" company="Paul Layne">
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

    public struct VertexVoxel : IDeserialize<VertexVoxel>
    {
        public byte Flags { get; set; } = 0;

        public byte[] Position { get; set; }

        public VertexVoxel(byte[] position)
        {
            this.Flags = 0;
            this.Position = position;
        }

        public static Dictionary<Range, VertexVoxel> DeserializeSparse(int length, BinaryReader reader)
        {
            var sparseVertices = new Dictionary<Range, VertexVoxel>();
            int i = 0;
            while (i < length)
            {
                byte flags = reader.ReadByte();
                int more = reader.ReadByte() + 1;
                if ((flags & 1) != 0)
                {
                    int j = 0;
                    while (j < more)
                    {
                        byte[] position = reader.ReadBytes(3);
                        int yetMore = reader.ReadByte() + 1;
                        int index = i + j;
                        sparseVertices[new Range(index, index + yetMore)] = new VertexVoxel(position)
                        {
                            Flags = (byte)(flags & 0xFE),
                        };
                        j += yetMore;
                    }

                    if (j != more)
                    {
                        throw new DeserializationException(DeserializeError.BadData);
                    }
                }

                i += more;
            }

            if (i != length)
            {
                throw new DeserializationException(DeserializeError.BadData);
            }

            return sparseVertices;
        }

        public void Deserialize(BinaryReader reader)
        {
            this.Flags = reader.ReadByte();
            this.Position = reader.ReadBytes(3);
        }
    }
}
