using System;
using System.Runtime.Serialization;

namespace CNBWinService
{
    [Serializable]
    public  class GraphCreationFailedException : Exception
    {
        public GraphCreationFailedException()
        {
        }

        public GraphCreationFailedException(string message) : base(message)
        {
        }

        public GraphCreationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GraphCreationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => String.Format("Bitmap could not be created with error: \n{0}", base.Message);

    }
}