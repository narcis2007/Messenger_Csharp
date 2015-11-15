using System;
using System.Runtime.Serialization;
using email_client.data.domain;

namespace email_client.data.repository
{
    [Serializable]
    internal class ValidationException : Exception
    {
        private Errors errors;

        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(Errors errors)
        {
            this.errors = errors;
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Errors getErrors()
        {
            return errors;
        }
    }
}