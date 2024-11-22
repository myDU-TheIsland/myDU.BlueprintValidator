// <copyright file="IDeserialize.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Interface
{
    using System.IO;

    public interface IDeserialize<T>
    {
        static abstract T Deserialize(Stream reader);
    }
}
