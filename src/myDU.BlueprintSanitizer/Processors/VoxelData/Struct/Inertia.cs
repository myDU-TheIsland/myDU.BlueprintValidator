// <copyright file="Inertia.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Struct
{
    using System.IO;
    using System.Numerics;
    using MyDU.BlueprintValidator.Processors.VoxelData.Extension;
    using MyDU.BlueprintValidator.Processors.VoxelData.Interface;

    public struct Inertia : IDeserialize<Inertia>
    {
        public double Mass { get; set; }

        public Vector3 GravityCenter { get; set; }

        public double[] InertiaTensor { get; set; }

        public static Inertia Deserialize(Stream reader)
        {
            double mass = DeserializationExtensions.DeserializeUInt64(reader);
            Vector3 gravityCenter = Vector3Extensions.Deserialize(reader);
            double[] inertiaTensor = new double[6];
            for (int i = 0; i < 6; i++)
            {
                inertiaTensor[i] = DeserializationExtensions.DeserializeDouble(reader);
            }

            return new Inertia
            {
                Mass = mass,
                GravityCenter = gravityCenter,
                InertiaTensor = inertiaTensor,
            };
        }
    }
}
