using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FUTAutoBuyer.Extensions;
using FUTAutoBuyer.Factories;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Params;

namespace FUTAutoBuyer
{
    public class FUTClient : IFUTClient
    {
        private readonly FUTAppRequestFactories _requestFactories = new FUTAppRequestFactories();

        public FUTAppRequestFactories RequestFactories { get { return _requestFactories; } }

        public async Task<LoginResponse> LoginAsync(LoginDetails loginDetails)
        {
            loginDetails.ThrowIfNullArgument();

            var loginRequest = _requestFactories.LoginRequestFactory(loginDetails);
            var loginResponse = await loginRequest.PerformRequestAsync();
            RequestFactories.PhishingToken = loginResponse.PhishingToken;
            RequestFactories.SessionId = loginResponse.SessionId;

            return loginResponse;
        }

        public Task<AuctionResponse> SearchAsync(SearchParams searchParameters)
        {
            searchParameters.ThrowIfNullArgument();

            return _requestFactories.SearchRequestFactory(searchParameters).PerformRequestAsync();
        }

        public Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0)
        {
            auctionInfo.ThrowIfNullArgument();

            if (bidAmount == 0)
            {
                bidAmount = auctionInfo.CalculateBid();
            }

            return _requestFactories.PlaceBidRequestFactory(auctionInfo, bidAmount).PerformRequestAsync();
        }

        public Task<Item> GetItemAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.PlayerRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.PlayerImageRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();

            return _requestFactories.TradeStatusRequestFactory(tradeIds).PerformRequestAsync();
        }

        public Task<CreditsResponse> GetCreditsAsync()
        {
            return _requestFactories.CreditsRequestFactory().PerformRequestAsync();
        }

        public Task<AuctionResponse> GetTradePileAsync()
        {
            return _requestFactories.TradePileRequestFactory().PerformRequestAsync();
        }

        public Task<WatchlistResponse> GetWatchlistAsync()
        {
            return _requestFactories.WatchlistRequestFactory().PerformRequestAsync();
        }

        public Task<PurchasedItemsResponse> GetPurchasedItemsAsync()
        {
            return _requestFactories.PurchasedItemsRequestFactory().PerformRequestAsync();
        }

        public Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails)
        {
            auctionDetails.ThrowIfNullArgument();

            return _requestFactories.ListAuctionFactory(auctionDetails).PerformRequestAsync();
        }

        public Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.RemoveFromWatchlistRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task RemoveFromTradePileAsync(AuctionInfo auctionInfo)
        {
            auctionInfo.ThrowIfNullArgument();

            return _requestFactories.RemoveFromTradePileRequestFactory(auctionInfo).PerformRequestAsync();
        }

        public Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData playerData)
        {
            playerData.ThrowIfNullArgument();

            return _requestFactories.SendItemToTradePileRequestFactory(playerData).PerformRequestAsync();
        }

        public Task<QuickSellResponse> QuickSellItemAsync(long itemId)
        {
            if (itemId < 1) throw new ArgumentException("Definitely not valid", "itemId");

            return _requestFactories.QuickSellRequestFactory(itemId).PerformRequestAsync();
        }
    }
}
