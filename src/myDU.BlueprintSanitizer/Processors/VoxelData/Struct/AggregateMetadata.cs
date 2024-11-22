// <copyright file="AggregateMetadata.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Collections.Generic;
    using System.IO;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct AggregateMetadata : IDeserialize<AggregateMetadata>
    {
        public LightMetadata LightCurrent { get; set; }

        public List<LightMetadata> LightChildren { get; set; }

        public HeavyMetadata HeavyCurrent { get; set; }

        public List<HeavyMetadata> HeavyChildren { get; set; } = new List<HeavyMetadata>(8);

        public const uint MAGIC = 0x9f81f3c0;
        public const uint VERSION = 8;

        public AggregateMetadata()
        {
            this.LightChildren = new List<LightMetadata>(8);
            this.HeavyChildren = new List<HeavyMetadata>(8);
        }

        public void Deserialize(BinaryReader reader)
        {
            uint magic = reader.ReadUInt32();
            Assertions.AssertMagic(magic, MAGIC);
            uint version = reader.ReadUInt32();
            Assertions.AssertVersion(version, VERSION);
            this.LightCurrent = SerializationExtensions.Deserialize<LightMetadata>(reader);
            this.LightChildren = new List<LightMetadata>(8);
            for (int i = 0; i < 8; i++)
            {
                this.LightChildren.Add(SerializationExtensions.Deserialize<LightMetadata>(reader));
            }

            this.HeavyCurrent = SerializationExtensions.Deserialize<HeavyMetadata>(reader);
            this.HeavyChildren = new List<HeavyMetadata>(8);
            for (int i = 0; i < 8; i++)
            {
                this.HeavyChildren.Add(SerializationExtensions.Deserialize<HeavyMetadata>(reader));
            }
        }
    }
}
