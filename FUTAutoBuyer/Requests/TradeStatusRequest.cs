using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class TradeStatusRequest : FUTAppRequestBase, IFUTRequest<AuctionResponse>
    {
        private readonly IEnumerable<long> _tradeIds;

        public TradeStatusRequest(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();
            _tradeIds = tradeIds;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            AddCommonHeaders();
            AddMethodOverrideHeader(HttpMethod.Get);
            var tradeStatusResponseMessage = await HttpClient
                .PostAsync(
                string.Format(Resources.FutHome + Resources.TradeStatus, string.Join("%2C", _tradeIds)),
                new StringContent(" ", Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

            return await Deserialize<AuctionResponse>(tradeStatusResponseMessage);
        }
    }
}
