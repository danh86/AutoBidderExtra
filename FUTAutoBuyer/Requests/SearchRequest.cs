using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Params;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class SearchRequest : FUTAppRequestBase, IFUTRequest<AuctionResponse>
    {
        private const byte PageSize = 12;

        private readonly SearchParams _searchParameters;

        public SearchRequest(SearchParams searchParameters)
        {
            searchParameters.ThrowIfNullArgument();
            _searchParameters = searchParameters;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.TransferMarket + "?start={0}&num={1}", (_searchParameters.Page - 1) * PageSize, PageSize + 1);
            _searchParameters.BuildUriString(ref uriString);
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var searchResponseMessage = await HttpClient
                .PostAsync(uriString, new StringContent(" "))
                .ConfigureAwait(false);

            return await Deserialize<AuctionResponse>(searchResponseMessage);
        }
    }
}
