// <copyright file="MaterialId.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System;
    using System.IO;
    using System.Text;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct MaterialId : IDeserialize<MaterialId>, IEquatable<MaterialId>, IComparable<MaterialId>
    {
        public ulong Id { get; set; }

        public string ShortName { get; set; }

        public MaterialId(ulong id, string shortName)
        {
            this.Id = id;
            this.ShortName = shortName;
        }

        public static MaterialId Deserialize(Stream reader)
        {
            ulong id = DeserializationExtensions.DeserializeUInt64(reader);
            byte[] shortNameData = new byte[8];
            reader.Read(shortNameData, 0, 8);
            string shortName = Encoding.ASCII.GetString(shortNameData);
            return new MaterialId(id, shortName);
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

        public int CompareTo(MaterialId other)
        {
            if (this.Id < other.Id)
            {
                return 1;
            }

            if (this.Id > other.Id)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
