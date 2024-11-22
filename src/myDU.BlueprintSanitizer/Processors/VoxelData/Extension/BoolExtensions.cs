// <copyright file="BoolExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System;

    public static class BoolExtensions
    {
        public static byte MaybeBoolToInt(bool? value)
        {
            return value switch
            {
                true => 1,
                false => 2,
                null => 0,
            };
        }

        public static bool? IntToMaybeBool(byte value)
        {
            return value switch
            {
                0 => null,
                1 => true,
                2 => false,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
            };
        }
    }
}
