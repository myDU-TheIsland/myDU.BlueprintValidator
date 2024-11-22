// <copyright file="SerializationException.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Exception
{
    using System;
    using MyDU.BlueprintValidator.Processors.VoxelData.Enum;

    public class SerializationException : Exception
    {
        public SerializeError ErrorType { get; }

        public new Exception InnerException { get; }

        public SerializationException(SerializeError errorType, Exception innerException = null)
        {
            this.ErrorType = errorType;
            this.InnerException = innerException;
        }

        public static SerializationException FromException(Exception ex)
        {
            return new SerializationException(SerializeError.Internal, ex);
        }
    }
}
