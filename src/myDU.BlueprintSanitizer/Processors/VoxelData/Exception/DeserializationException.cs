// <copyright file="DeserializationException.cs" company="Paul Layne">
// Copyright (c) Paul Layne. All rights reserved.
// </copyright>

namespace MyDU.BlueprintValidator.Processors.VoxelData.Exception
{
    using System;
    using MyDU.BlueprintValidator.Processors.VoxelData.Enum;

    public class DeserializationException : Exception
    {
        public DeserializeError ErrorType { get; }

        public new Exception InnerException { get; }

        public uint ExpectedValue { get; }

        public uint ActualValue { get; }

        public DeserializationException(DeserializeError errorType, Exception innerException = null)
        {
            this.ErrorType = errorType;
            this.InnerException = innerException;
        }

        public DeserializationException(DeserializeError errorType, uint actual, uint expected)
        {
            this.ErrorType = errorType;
            this.ExpectedValue = expected;
            this.ActualValue = actual;
        }
    }
}
