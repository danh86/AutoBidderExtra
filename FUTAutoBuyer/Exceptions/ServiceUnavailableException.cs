using System;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Exceptions
{
    public class ServiceUnavailableException : PermissionDeniedException
    {
        public ServiceUnavailableException(FUTAppErrorWithDebugStr futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
