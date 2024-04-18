using System;
using System.Runtime.Serialization;

namespace CNBWinService
{
    [Serializable]
    internal class XMLExportFailedException : Exception
    {
        public XMLExportFailedException()
        {
        }

        public XMLExportFailedException(string message) : base(message)
        {
        }

        public XMLExportFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XMLExportFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => String.Format("XML cannot be created with error: \n{0}", base.Message);

    }
}