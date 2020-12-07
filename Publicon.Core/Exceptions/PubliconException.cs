using System;

namespace Publicon.Core.Exceptions
{
    public class PubliconException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public PubliconException(ErrorCode errorCode)
            : this(errorCode, string.Empty) { }

        public PubliconException(ErrorCode errorCode, string message)
            : this(errorCode, message, null) { }

        public PubliconException(ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }

}
