using System.Net.Http;
using System.Threading.Tasks;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Requests
{
    internal class SendItemToTradePileRequest : FUTAppRequestBase, IFUTRequest<SendItemToTradePileResponse>
    {
        private readonly ItemData _playerData;

        public SendItemToTradePileRequest(ItemData playerData)
        {
            playerData.ThrowIfNullArgument();
            _playerData = playerData;
        }

        public async Task<SendItemToTradePileResponse> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Put);
            AddCommonHeaders();
            var content = string.Format("{{\"itemData\":[{{\"id\":\"{0}\",\"pile\":\"trade\"}}]}}", _playerData.Id);
            var tradepileResponseMessage = await HttpClient
                .PostAsync(string.Format(Resources.FutHome + Resources.ListItem), new StringContent(content))
                .ConfigureAwait(false);

            return await Deserialize<SendItemToTradePileResponse>(tradepileResponseMessage);
        }
    }
}
