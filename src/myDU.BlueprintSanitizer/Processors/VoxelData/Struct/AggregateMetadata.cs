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

        public AggregateMetadata(LightMetadata lightCurrent, HeavyMetadata heavyCurrent)
        {
            this.LightCurrent = lightCurrent;
            this.HeavyCurrent = heavyCurrent;
            this.LightChildren = new List<LightMetadata>(8);
            this.HeavyChildren = new List<HeavyMetadata>(8);
        }

        public static AggregateMetadata Deserialize(Stream reader)
        {
            uint magic = DeserializationExtensions.DeserializeUInt32(reader);
            Assertions.AssertMagic(magic, MAGIC);
            uint version = DeserializationExtensions.DeserializeUInt32(reader);
            Assertions.AssertVersion(version, VERSION);
            LightMetadata lightCurrent = LightMetadata.Deserialize(reader);
            List<LightMetadata> lightChildren = new List<LightMetadata>(8);
            for (int i = 0; i < 8; i++)
            {
                lightChildren.Add(LightMetadata.Deserialize(reader));
            }

            HeavyMetadata heavyCurrent = HeavyMetadata.Deserialize(reader);
            List<HeavyMetadata> heavyChildren = new List<HeavyMetadata>(8);
            for (int i = 0; i < 8; i++)
            {
                heavyChildren.Add(HeavyMetadata.Deserialize(reader));
            }

            return new AggregateMetadata(lightCurrent, heavyCurrent)
            {
                LightChildren = lightChildren,
                HeavyChildren = heavyChildren,
            };
        }
    }
}
