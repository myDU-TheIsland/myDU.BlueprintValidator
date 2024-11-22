// <copyright file="Inertia.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.Collections.Generic;
    using System.IO;
    using System.Numerics;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct Inertia : IDeserialize<Inertia>
    {
        public double Mass { get; set; }

        public Vector3 GravityCenter { get; set; }

        public List<double> InertiaTensor { get; set; }

        public void Deserialize(BinaryReader reader)
        {
            this.Mass = reader.ReadDouble();
            this.GravityCenter = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            this.InertiaTensor = new List<double>();
            for (int i = 0; i < 6; i++)
            {
                this.InertiaTensor.Add(reader.ReadDouble());
            }
        }
    }
}
