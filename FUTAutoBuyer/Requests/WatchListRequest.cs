using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Requests
{
    internal class WatchlistRequest : FUTAppRequestBase, IFUTRequest<WatchlistResponse>
    {
        public async Task<WatchlistResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var watchlistResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.Watchlist))
                .ConfigureAwait(false);

            return await Deserialize<WatchlistResponse>(watchlistResponseMessage);
        }
    }
}
