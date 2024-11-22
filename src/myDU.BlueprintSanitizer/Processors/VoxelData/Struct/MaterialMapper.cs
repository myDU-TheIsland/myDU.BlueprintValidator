// <copyright file="MaterialMapper.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct MaterialMapper : IDeserialize<MaterialMapper>
    {
        public SortedDictionary<MaterialId, byte> Mapping;
        public SortedDictionary<byte, MaterialId> ReverseMapping;

        public void Deserialize(BinaryReader reader)
        {
            this.Mapping = DictionaryExtensions.Deserialize<MaterialId>(reader);
            this.ReverseMapping = new SortedDictionary<byte, MaterialId>(this.Mapping.ToDictionary(key => key.Value, value => value.Key));
        }
    }
}
