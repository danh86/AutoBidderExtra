using FUTAutoBuyer.Extensions;

namespace FUTAutoBuyer.Entities
{
    public class LoginDetails
    {
        public string Username { get; private set; }

        public string Password { get; private set; }

        public string SecretAnswer { get; private set; }

        public PlayerPlatform PlayerPlatform { get; set; }

        public LoginDetails(string username, string password, string secretAnswer, PlayerPlatform playerPlatform)
        {
            username.ThrowIfInvalidArgument();
            password.ThrowIfInvalidArgument();
            secretAnswer.ThrowIfInvalidArgument();
            Username = username;
            Password = password;
            SecretAnswer = secretAnswer;
            PlayerPlatform = playerPlatform;
        }
    }
}
