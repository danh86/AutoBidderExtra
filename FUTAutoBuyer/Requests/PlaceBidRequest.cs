using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class PlaceBidRequest : FUTAppRequestBase, IFUTRequest<AuctionResponse>
    {
        private readonly AuctionInfo _auctionInfo;

        private readonly uint _bidAmount;

        public PlaceBidRequest(AuctionInfo auctionInfo, uint bidAmount)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
            _bidAmount = bidAmount;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Put);
            AddCommonHeaders();
            var content = string.Format("{{\"bid\":{0}}}", _bidAmount);
            var bidResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.FutHome + Resources.Bid, _auctionInfo.TradeId), new StringContent(content))
                .ConfigureAwait(false);

            return await Deserialize<AuctionResponse>(bidResponseMessage);
        }
    }
}
