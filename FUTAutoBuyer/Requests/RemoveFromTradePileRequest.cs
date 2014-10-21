using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class RemoveFromTradePileRequest : FUTAppRequestBase, IFUTRequest<byte>
    {
        private readonly AuctionInfo _auctioninfo;

        public RemoveFromTradePileRequest(AuctionInfo auctioninfo)
        {
            auctioninfo.ThrowIfNullArgument();
            _auctioninfo = auctioninfo;
        }

        public async Task<byte> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.RemoveFromTradePile, _auctioninfo.TradeId);

            AddMethodOverrideHeader(HttpMethod.Delete);
            AddCommonHeaders();
            var removeFromTradePileMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);
            removeFromTradePileMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
