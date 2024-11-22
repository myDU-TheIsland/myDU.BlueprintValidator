// <copyright file="Assertions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System.IO;

    public static class Assertions
    {
        public static void AssertMagic(uint actual, uint expected)
        {
            if (actual != expected)
            {
                throw new InvalidDataException("Bad Magic Data");
            }
        }

        public static void AssertVersion(uint actual, uint expected)
        {
            if (actual != expected)
            {
                throw new InvalidDataException("Bad Version Data");
            }
        }
    }
}
