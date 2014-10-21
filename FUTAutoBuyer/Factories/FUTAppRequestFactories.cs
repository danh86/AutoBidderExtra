using System;
using System.Collections.Generic;
using System.Net;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Params;
using FUTAutoBuyer.Requests;
using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Factories
{
    public class FUTAppRequestFactories
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        private string _phishingToken;

        private string _sessionId;

        public string PhishingToken
        {
            get { return _phishingToken; }
            set
            {
                value.ThrowIfInvalidArgument();
                _phishingToken = value;
            }
        }

        public string SessionId
        {
            get { return _sessionId; }
            set
            {
                value.ThrowIfInvalidArgument();
                _sessionId = value;
            }
        }

        private Func<LoginDetails, IFUTRequest<LoginResponse>> _loginRequestFactory;

        private Func<SearchParams, IFUTRequest<AuctionResponse>> _searchRequestFactory;

        private Func<AuctionInfo, uint, IFUTRequest<AuctionResponse>> _placeBidRequestFactory;

        private Func<AuctionInfo, IFUTRequest<Item>> _playerRequestFactory;

        private Func<AuctionInfo, IFUTRequest<byte[]>> _playerImageRequestFactory;

        private Func<IEnumerable<long>, IFUTRequest<AuctionResponse>> _tradeStatusRequestFactory;

        private Func<IFUTRequest<CreditsResponse>> _creditsRequestFactory;

        private Func<IFUTRequest<AuctionResponse>> _tradePileRequestFactory;

        private Func<IFUTRequest<WatchlistResponse>> _watchlistRequestFactory;

        private Func<IFUTRequest<PurchasedItemsResponse>> _purchaseditemsRequestFactory;

        private Func<AuctionDetails, IFUTRequest<ListAuctionResponse>> _listAuctionRequestFactory;

        private Func<AuctionInfo, IFUTRequest<byte>> _removeFromWatchlistRequestFactory;

        private Func<AuctionInfo, IFUTRequest<byte>> _removeFromTradePileRequestFactory;

        private Func<ItemData, IFUTRequest<SendItemToTradePileResponse>> _sendItemToTradePileRequestFactory;

        private Func<long, IFUTRequest<QuickSellResponse>> _quickSellRequestFactory;        

        public Func<LoginDetails, IFUTRequest<LoginResponse>> LoginRequestFactory
        {
            get
            {
                return _loginRequestFactory ?? (_loginRequestFactory = details =>
                {
                    var loginRequest = new LoginRequest(details);
                    loginRequest.SetCookieContainer(_cookieContainer);
                    return loginRequest;
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _loginRequestFactory = value;
            }
        }

        public Func<SearchParams, IFUTRequest<AuctionResponse>> SearchRequestFactory
        {
            get
            {
                return _searchRequestFactory ?? (_searchRequestFactory = parameters => new SearchRequest(parameters)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _searchRequestFactory = value;
            }
        }

        public Func<AuctionInfo, uint, IFUTRequest<AuctionResponse>> PlaceBidRequestFactory
        {
            get
            {
                return _placeBidRequestFactory ?? (_placeBidRequestFactory = (info, amount) => new PlaceBidRequest(info, amount)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _placeBidRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFUTRequest<Item>> PlayerRequestFactory
        {
            get
            {
                return _playerRequestFactory ?? (_playerRequestFactory = info => new ItemRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _playerRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFUTRequest<byte[]>> PlayerImageRequestFactory
        {
            get
            {
                return _playerImageRequestFactory ?? (_playerImageRequestFactory = info => new PlayerImageRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _playerImageRequestFactory = value;
            }
        }

        public Func<IEnumerable<long>, IFUTRequest<AuctionResponse>> TradeStatusRequestFactory
        {
            get
            {
                return _tradeStatusRequestFactory ?? (_tradeStatusRequestFactory = tradeIds => new TradeStatusRequest(tradeIds)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _tradeStatusRequestFactory = value;
            }
        }

        public Func<IFUTRequest<CreditsResponse>> CreditsRequestFactory
        {
            get
            {
                return _creditsRequestFactory ?? (_creditsRequestFactory = () => new CreditsRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _creditsRequestFactory = value;
            }
        }

        public Func<IFUTRequest<AuctionResponse>> TradePileRequestFactory
        {
            get
            {
                return _tradePileRequestFactory ?? (_tradePileRequestFactory = () => new TradePileRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _tradePileRequestFactory = value;
            }
        }

        public Func<IFUTRequest<WatchlistResponse>> WatchlistRequestFactory
        {
            get
            {
                return _watchlistRequestFactory ?? (_watchlistRequestFactory = () => new WatchlistRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _watchlistRequestFactory = value;
            }
        }

        public Func<IFUTRequest<PurchasedItemsResponse>> PurchasedItemsRequestFactory
        {
            get
            {
                return _purchaseditemsRequestFactory ?? (_purchaseditemsRequestFactory = () => new PurchasedItemsRequest
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _purchaseditemsRequestFactory = value;
            }
        }

        public Func<AuctionDetails, IFUTRequest<ListAuctionResponse>> ListAuctionFactory
        {
            get
            {
                return _listAuctionRequestFactory ?? (_listAuctionRequestFactory = details => new ListAuctionRequest(details)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _listAuctionRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFUTRequest<byte>> RemoveFromWatchlistRequestFactory
        {
            get
            {
                return _removeFromWatchlistRequestFactory ?? (_removeFromWatchlistRequestFactory = info => new RemoveFromWatchListRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _removeFromWatchlistRequestFactory = value;
            }
        }

        public Func<AuctionInfo, IFUTRequest<byte>> RemoveFromTradePileRequestFactory
        {
            get
            {
                return _removeFromTradePileRequestFactory ?? (_removeFromTradePileRequestFactory = info => new RemoveFromTradePileRequest(info)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _removeFromTradePileRequestFactory = value;
            }
        }

        public Func<ItemData, IFUTRequest<SendItemToTradePileResponse>> SendItemToTradePileRequestFactory
        {
            get
            {
                return _sendItemToTradePileRequestFactory ?? (_sendItemToTradePileRequestFactory = itemData => new SendItemToTradePileRequest(itemData)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _sendItemToTradePileRequestFactory = value;
            }
        }

        public Func<long, IFUTRequest<QuickSellResponse>> QuickSellRequestFactory
        {
            get
            {
                return _quickSellRequestFactory ?? (_quickSellRequestFactory = itemId => new QuickSellRequest(itemId)
                {
                    PhishingToken = PhishingToken,
                    SessionId = SessionId
                });
            }
            set
            {
                value.ThrowIfNullArgument();
                _quickSellRequestFactory = value;
            }
        }
    }
}
