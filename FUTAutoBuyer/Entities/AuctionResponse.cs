using System.Collections.Generic;

namespace FUTAutoBuyer.Entities
{
    public class AuctionResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public BidTokens BidTokens { get; set; }

        public uint Credits { get; set; }

        public List<Currency> Currencies { get; set; }

        public List<DupeItem> DuplicateItemIdList { get; set; }

        public string ErrorState { get; set; }
    }
}
