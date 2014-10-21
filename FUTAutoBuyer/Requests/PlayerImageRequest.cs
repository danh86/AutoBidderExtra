using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class PlayerImageRequest : FUTAppRequestBase, IFUTRequest<byte[]>
    {
        private readonly AuctionInfo _auctionInfo;

        public PlayerImageRequest(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();
            _auctionInfo = auctionInfo;
        }

        public async Task<byte[]> PerformRequestAsync()
        {
            AddUserAgent();
            AddAcceptHeader("*/*");
            AddReferrerHeader(Resources.BaseShowoff);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();

            return await HttpClient
                .GetByteArrayAsync(string.Format(Resources.PlayerImage, _auctionInfo.CalculateBaseId()))
                .ConfigureAwait(false);
        }
    }
}
