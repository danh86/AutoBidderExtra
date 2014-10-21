using System.Collections.Generic;

namespace FUTAutoBuyer.Entities
{
    public class PurchasedItemsResponse
    {
        public List<DupeItem> DuplicateItemIdList { get; set; }

        public List<ItemData> ItemData { get; set; }
    }
}
