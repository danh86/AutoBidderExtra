using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Requests
{
    internal class PurchasedItemsRequest : FUTAppRequestBase, IFUTRequest<PurchasedItemsResponse>
    {
        public async Task<PurchasedItemsResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var purchasedItemsMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.PurchasedItems))
                .ConfigureAwait(false);

            return await Deserialize<PurchasedItemsResponse>(purchasedItemsMessage);
        }
    }
}
