using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Constants;

namespace FUTAutoBuyer.Requests
{
    internal class CreditsRequest : FUTAppRequestBase, IFUTRequest<CreditsResponse>
    {
        public async Task<CreditsResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var creditsResponseMessage = await HttpClient
                .GetAsync(string.Format(Resources.FutHome + Resources.Credits))
                .ConfigureAwait(false);

            return await Deserialize<CreditsResponse>(creditsResponseMessage);
        }
    }
}
