using System.Collections.Generic;

namespace FUTAutoBuyer.Entities
{
    public class QuickSellResponse
    {
        public List<ItemData> Items { get; set; }

        public uint TotalCredits { get; set; }
    }
}
