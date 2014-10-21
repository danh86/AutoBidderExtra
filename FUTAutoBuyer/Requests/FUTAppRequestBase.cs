using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FUTAutoBuyer.Constants;
using FUTAutoBuyer.Extensions;
using FUTAutoBuyer.Exceptions;
using FUTAutoBuyer.Entities;

namespace FUTAutoBuyer.Requests
{
    public abstract class FUTAppRequestBase
    {
        private string _phishingToken;

        private string _sessionId;

        private IHttpClient _httpClient;

        public string PhishingToken
        {
            set
            {
                value.ThrowIfInvalidArgument();
                _phishingToken = value;
            }
        }

        public string SessionId
        {
            set
            {
                value.ThrowIfInvalidArgument();
                _sessionId = value;
            }
        }

        internal IHttpClient HttpClient
        {
            get { return _httpClient ?? (_httpClient = new HttpClientWrapper()); }
            set
            {
                value.ThrowIfNullArgument();
                _httpClient = value;
            }
        }

        protected FUTAppRequestBase()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error };
            HttpClient.SetExpectContinueHeaderToFalse();
        }

        protected void AddCommonHeaders()
        {
            HttpClient.AddRequestHeader(FUTAppHeaders.PhishingToken, _phishingToken);
            HttpClient.AddRequestHeader(FUTAppHeaders.EmbedError, "true");
            HttpClient.AddRequestHeader(FUTAppHeaders.SessionId, _sessionId);
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("application/json");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddReferrerHeader("http://www.easports.com/iframe/fut/bundles/futweb/web/flash/FifaUltimateTeam.swf");
            AddUserAgent();
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddUserAgent()
        {
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
        }

        protected void AddAcceptHeader(string value)
        {
            HttpClient.AddRequestHeader(HttpHeaders.Accept, value);
        }

        protected void AddReferrerHeader(string value)
        {
            HttpClient.SetReferrerUri(value);
        }

        protected void AddAcceptEncodingHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptEncoding, "gzip,deflate,sdch");
        }

        protected void AddAcceptLanguageHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8");
        }

        protected void AddMethodOverrideHeader(HttpMethod httpMethod)
        {
            HttpClient.AddRequestHeader(FUTAppHeaders.MethodOverride, httpMethod.Method);
        }

        protected static async Task<T> Deserialize<T>(HttpResponseMessage message) where T : class
        {
            message.EnsureSuccessStatusCode();
            var messageContent = await message.Content.ReadAsStringAsync();
            T deserializedObject = null;

            try
            {
                deserializedObject = JsonConvert.DeserializeObject<T>(messageContent);
            }
            catch (JsonSerializationException serializationException)
            {
                try
                {
                    var futErrorWithDebugString = JsonConvert.DeserializeObject<FUTAppErrorWithDebugStr>(messageContent);
                    MapAndThrowException(serializationException, futErrorWithDebugString);
                }
                catch (JsonSerializationException)
                {
                    try
                    {
                        var futErrorWithMessage = JsonConvert.DeserializeObject<FUTAppErrorWithMsg>(messageContent);
                        MapAndThrowException(serializationException, futErrorWithMessage);
                    }
                    catch (JsonSerializationException)
                    {
                        throw serializationException;
                    }
                }
            }

            return deserializedObject;
        }

        private static void MapAndThrowException(Exception exception, FUTAppErrorWithDebugStr futError)
        {
            switch (futError.Code)
            {
                case ErrorCodes.BadRequest:
                    throw new BadRequestException(futError, exception);
                case ErrorCodes.PermissionDenied:
                    throw new PermissionDeniedException(futError, exception);
                case ErrorCodes.InternalServerError:
                    throw new InternalServerException(futError, exception);
                case ErrorCodes.ServiceUnavailable:
                    throw new ServiceUnavailableException(futError, exception);
                default:
                    throw new FUTAppException(string.Format("Unknown EA error, please report - Code: {0}, String: {1}", futError.Code, futError.String), exception);
            }
        }

        private static void MapAndThrowException(Exception exception, FUTAppErrorWithMsg futError)
        {
            switch (futError.Code)
            {
                case ErrorCodes.ExpiredSession:
                    throw new ExpiredSessionException(futError, exception);
                default:
                    throw new FUTAppException(string.Format("Unknown EA error, please report - Code: {0}, Reason: {1}", futError.Code, futError.Reason), exception);
            }
        }
    }
}
