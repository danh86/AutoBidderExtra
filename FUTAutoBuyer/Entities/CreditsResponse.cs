using System.Collections.Generic;

namespace FUTAutoBuyer.Entities
{
    public class CreditsResponse
    {
        public uint Credits { get; set; }

        public List<Currency> Currencies { get; set; }

        public UnopenedPacks UnopenedPacks { get; set; }

        public double FutCashBalance { get; set; }
    }
}
