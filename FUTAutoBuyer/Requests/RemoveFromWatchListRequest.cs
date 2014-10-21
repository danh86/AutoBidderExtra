using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class RemoveFromWatchListRequest : FUTAppRequestBase, IFUTRequest<byte>
    {
        private readonly AuctionInfo _auctioninfo;

        public RemoveFromWatchListRequest(AuctionInfo auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.Watchlist + "?tradeId={0}", _auctioninfo.TradeId);
            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            var removeFromWatchlistResponseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            removeFromWatchlistResponseMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
