using System.Collections.Generic;

namespace FUTAutoBuyer.Entities
{
    public class WatchlistResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public uint Credits { get; set; }

        public List<DupeItem> DuplicateItemIdList { get; set; }

        public ushort Total { get; set; }
    }
}
