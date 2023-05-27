using System.Text.Json.Serialization;
namespace BookShop.Test.UnitTest.SettingsModels
{
    public class AdminSettings
    {
        [JsonRequired]
        public string Username { get; set; }

        [JsonRequired]
        public string Password { get; set; }

        [JsonRequired]
        public string GrantType { get; set; }

        [JsonRequired]
        public string ClientId { get; set; }

        [JsonRequired]
        public string ClientSecret { get; set; }

        [JsonRequired]
        public int Domain { get; set; }
    }
}
