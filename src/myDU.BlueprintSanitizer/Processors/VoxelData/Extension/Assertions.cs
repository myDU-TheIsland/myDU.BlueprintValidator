// <copyright file="Assertions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using MyDU.BlueprintValidator.Processors.VoxelData.Exception;

    public static class Assertions
    {
        public static void AssertMagic(uint actual, uint expected)
        {
            if (actual != expected)
            {
                throw new DeserializationException(VoxelData.Enum.DeserializeError.BadData, actual, expected);
            }
        }

        public static void AssertVersion(uint actual, uint expected)
        {
            if (actual != expected)
            {
                throw new DeserializationException(VoxelData.Enum.DeserializeError.BadData, actual, expected);
            }
        }
    }
}
