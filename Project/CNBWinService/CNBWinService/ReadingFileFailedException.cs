using System;
using System.Runtime.Serialization;

namespace CNBWinService
{
    [Serializable]
    public class ReadingFileFailedException : Exception
    {
        public ReadingFileFailedException()
        {
        }

        public ReadingFileFailedException(string message) : base(message)
        {
        }

        public ReadingFileFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReadingFileFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message => String.Format("File could not be read with error: \n{0}",base.Message);

    }
}