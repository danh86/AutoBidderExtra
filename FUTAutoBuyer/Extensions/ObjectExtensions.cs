using System;

namespace FUTAutoBuyer.Extensions
{
    internal static class ObjectExtensions
    {
        public static void ThrowIfNullArgument(this object input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
