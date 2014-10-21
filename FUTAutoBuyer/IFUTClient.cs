using System.Collections.Generic;
using System.Threading.Tasks;
using FUTAutoBuyer.Factories;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Params;

namespace FUTAutoBuyer
{
    public interface IFUTClient
    {
        FUTAppRequestFactories RequestFactories { get; }

        Task<LoginResponse> LoginAsync(LoginDetails loginDetails);

        Task<AuctionResponse> SearchAsync(SearchParams searchParameters);

        Task<AuctionResponse> PlaceBidAsync(AuctionInfo auctionInfo, uint bidAmount = 0);

        Task<Item> GetItemAsync(AuctionInfo auctionInfo);

        Task<byte[]> GetPlayerImageAsync(AuctionInfo auctionInfo);

        Task<AuctionResponse> GetTradeStatusAsync(IEnumerable<long> tradeIds);

        Task<CreditsResponse> GetCreditsAsync();

        Task<AuctionResponse> GetTradePileAsync();

        Task<WatchlistResponse> GetWatchlistAsync();

        Task<PurchasedItemsResponse> GetPurchasedItemsAsync();

        Task<ListAuctionResponse> ListAuctionAsync(AuctionDetails auctionDetails);

        Task RemoveFromWatchlistAsync(AuctionInfo auctionInfo);

        Task RemoveFromTradePileAsync(AuctionInfo auctionInfo);

        Task<SendItemToTradePileResponse> SendItemToTradePileAsync(ItemData playerData);

        Task<QuickSellResponse> QuickSellItemAsync(long itemId);
    }
}
