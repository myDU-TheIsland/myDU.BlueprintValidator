// <copyright file="VoxelData.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Classes
{
    using System;
    using MyDU.BlueprintValidator.Processors.VoxelData.Struct;

    public class VoxelData
    {
        public byte[] data { get; set; }

        public VoxelCellData actualCellData { get; set; }

        public long hash { get; set; }

        public DateTime updated_at { get; set; }
    }
}
