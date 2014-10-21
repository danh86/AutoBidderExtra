using System;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Exceptions
{
    public class PermissionDeniedException : FUTAppException
    {
        public FUTAppErrorWithDebugStr FutError { get; private set; }

        public PermissionDeniedException(FUTAppErrorWithDebugStr futError, Exception exception)
            : base(futError.Reason, exception)
        {
            futError.ThrowIfNullArgument();
            FutError = futError;
        }
    }
}
