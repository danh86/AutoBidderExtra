using System;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Exceptions
{
    public class BadRequestException : PermissionDeniedException
    {
        public BadRequestException(FUTAppErrorWithDebugStr futAppError, Exception exception)
            : base(futAppError, exception)
        {
        }
    }
}
