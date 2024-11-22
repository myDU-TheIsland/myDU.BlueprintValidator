// <copyright file="VertexVoxel.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct VertexVoxel : IDeserialize<VertexVoxel>
    {
        public byte Flags;
        public byte[] Position;

        public VertexVoxel(byte[] position)
        {
            this.Flags = 0;
            this.Position = position;
        }

        public static VertexVoxel Deserialize(Stream reader)
        {
            byte flags = (byte)reader.ReadByte();
            byte[] position = new byte[3];
            reader.Read(position, 0, position.Length);
            return new VertexVoxel(position) { Flags = flags };
        }

        public static SortedDictionary<int, VertexVoxel> DeserializeSparse(int length, Stream reader)
        {
            var sparseVertices = new SortedDictionary<int, VertexVoxel>();
            int i = 0;
            while (i < length)
            {
                byte flags = (byte)reader.ReadByte();
                byte maskedFlags = (byte)(flags & 0xFE);
                int more = reader.ReadByte() + 1;
                if ((flags & 1) != 0)
                {
                    int j = 0;
                    while (j < more)
                    {
                        byte[] position = new byte[3];
                        reader.Read(position, 0, 3);
                        int yetMore = reader.ReadByte() + 1;
                        int index = i + j;
                        sparseVertices.Add(
                            index,
                            new VertexVoxel(position)
                            {
                                Flags = maskedFlags,
                            });
                        j += yetMore;
                    }

                    if (j != more)
                    {
                        throw new InvalidDataException("Bad data");
                    }
                }

                i += more;
            }

            if (i != length)
            {
                throw new InvalidDataException("Bad data");
            }

            return sparseVertices;
        }
    }
}
