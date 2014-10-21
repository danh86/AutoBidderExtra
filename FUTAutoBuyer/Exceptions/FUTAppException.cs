using System;

namespace FUTAutoBuyer.Exceptions
{
    public class FUTAppException : Exception
    {
        public FUTAppException(string message)
            : base(message)
        {
        }

        public FUTAppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
