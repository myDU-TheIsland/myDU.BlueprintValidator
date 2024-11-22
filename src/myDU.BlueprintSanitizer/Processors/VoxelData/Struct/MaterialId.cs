// <copyright file="MaterialId.cs" company="Paul Layne">
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
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct MaterialId : IDeserialize<MaterialId>, IEquatable<MaterialId>
    {
        public ulong Id { get; set; }

        public string ShortName { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            this.Id = reader.ReadUInt64();
            byte[] shortNameData = reader.ReadBytes(8);
            this.ShortName = Encoding.ASCII.GetString(shortNameData);
        }

        public bool Equals(MaterialId other)
        {
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is MaterialId other && this.Equals(other);
        }

        public static bool operator ==(MaterialId left, MaterialId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MaterialId left, MaterialId right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }
    }
}
