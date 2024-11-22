// <copyright file="CompressionExtensions.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Extension
{
    using System;
    using System.IO;
    using K4os.Compression.LZ4;
    using MyDU.BlueprintValidator.Processors.VoxelData.Exception;
    using MyDU.BlueprintValidator.Processors.VoxelData.Struct;

    public static class CompressionExtensions
    {
        private const uint COMPRESSEDMAGIC = 0xfb14b6f9;

        public static VoxelCellData Decompress(byte[] bytes)
        {
            uint magic = BitConverter.ToUInt32(bytes, 0);
            Assertions.AssertMagic(magic, COMPRESSEDMAGIC);
            int uncompressedSize = BitConverter.ToInt32(bytes, 4);
            byte[] target = new byte[uncompressedSize];
            int compressedSize = bytes.Length - 12;

            int decoded = LZ4Codec.Decode(bytes, 12, compressedSize, target, 0, target.Length);

            if (decoded != uncompressedSize)
            {
                throw new DeserializationException(VoxelData.Enum.DeserializeError.BadData);
            }

            using (var memoryStream = new MemoryStream(target, 0, target.Length))
            {
                BinaryReader reader = new BinaryReader(memoryStream);
                var output = new VoxelCellData();
                var testData = System.Convert.ToBase64String(target);
                output.Deserialize(reader);
                return output;
            }
        }
    }
}