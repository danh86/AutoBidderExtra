using System;
using FUTAutoBuyer.Extensions;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Exceptions
{
    public class InternalServerException : FUTAppException
    {
        public FUTAppErrorWithDebugStr FutError { get; private set; }

        public InternalServerException(FUTAppErrorWithDebugStr futError, Exception exception)
            : base(futError.Reason, exception)
        {
            futError.ThrowIfNullArgument();
            FutError = futError;
        }
    }
}
