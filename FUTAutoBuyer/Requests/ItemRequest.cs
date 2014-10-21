using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Extensions;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Requests
{
    internal class ItemRequest : FUTAppRequestBase, IFUTRequest<Item>
    {
        private readonly AuctionInfo _auctionInfo;

        public ItemRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
        }

        public async Task<Item> PerformRequestAsync()
        {
            AddUserAgent();
            AddAcceptHeader("*/*");
            AddReferrerHeader(Resources.BaseShowoff);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            var itemResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.Item, _auctionInfo.CalculateBaseId()))
                .ConfigureAwait(false);
            var itemWrapper = await Deserialize<PlayerWrapper>(itemResponseMessage);

            return itemWrapper.Item;
        }
    }
}
