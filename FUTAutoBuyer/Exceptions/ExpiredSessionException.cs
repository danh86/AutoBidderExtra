using System;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Exceptions
{
    public class ExpiredSessionException : FUTAppException
    {
        public FUTAppErrorWithMsg FutError { get; private set; }

        public ExpiredSessionException(FUTAppErrorWithMsg futError, Exception exception)
            : base(futError.Message, exception)
        {
            futError.ThrowIfNullArgument();
            FutError = futError;
        }
    }
}
