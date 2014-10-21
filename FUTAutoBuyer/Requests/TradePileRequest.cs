using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Requests
{
    internal class TradePileRequest : FUTAppRequestBase, IFUTRequest<AuctionResponse>
    {
        public async Task<AuctionResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var tradePileResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.TradePile))
                .ConfigureAwait(false);

            return await Deserialize<AuctionResponse>(tradePileResponseMessage);
        }
    }
}
