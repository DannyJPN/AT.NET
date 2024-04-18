using System;
using System.Runtime.Serialization;

namespace CNBWinService
{
    [Serializable]
    public class DownloadFailedException : Exception
    {
        public DownloadFailedException()
        {
        }

        public DownloadFailedException(string message) : base(message)
        {
        }

        public DownloadFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DownloadFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => String.Format("Download failed with error: \n{0}", base.Message);

    }
}