using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Entities
{
    public class LoginResponse
    {
        public string NucleusId { get; private set; }

        public Shards Shards { get; private set; }

        public UserAccount UserAccount { get; private set; }

        public string SessionId { get; private set; }

        public string PhishingToken { get; private set; }

        public LoginResponse(string nucleusId, Shards shards, UserAccount userAccount, string sessionId, string phishingToken)
        {
            nucleusId.ThrowIfInvalidArgument();
            shards.ThrowIfNullArgument();
            userAccount.ThrowIfNullArgument();
            sessionId.ThrowIfInvalidArgument();
            phishingToken.ThrowIfInvalidArgument();
            NucleusId = nucleusId;
            Shards = shards;
            UserAccount = userAccount;
            SessionId = sessionId;
            PhishingToken = phishingToken;
        }
    }
}
