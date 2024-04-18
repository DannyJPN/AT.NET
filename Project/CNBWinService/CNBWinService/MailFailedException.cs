using System;
using System.Runtime.Serialization;

namespace CNBWinService
{
    [Serializable]
   public class MailFailedException : Exception
    {
        public MailFailedException()
        {
        }

        public MailFailedException(string message) : base(message)
        {
        }

        public MailFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MailFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => String.Format("Mail could not be sent with error: \n{0}", base.Message);

    }
}