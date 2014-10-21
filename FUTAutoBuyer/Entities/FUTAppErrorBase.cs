using FUTAutoBuyer.Constants;

namespace FUTAutoBuyer.Entities
{
    public class FUTAppErrorBase
    {
        public string Reason { get; set; }

        public ErrorCodes Code { get; set; }
    }
}
