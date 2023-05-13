using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Net
{
    public class ClientApiIntraction : IClientApiIntraction
    {
        public virtual string JwtToken { get; set; }

        public virtual Guid DeviceId { get; set; }

        public virtual string ClientId { get; set; }

        public virtual string DomainUrl { get; set; }

        private void AddAccessToken(HttpClient client)
        {
            if (!string.IsNullOrEmpty(JwtToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            }
        }

        private void AddDeviceId(HttpClient httpClient)
        {
            if (DeviceId != Guid.Empty)
            {
                httpClient.DefaultRequestHeaders.Add("deviceId", DeviceId.ToString());
            }
        }

        private void AddClientId(HttpClient httpClient)
        {
            if (!string.IsNullOrEmpty(ClientId))
            {
                httpClient.DefaultRequestHeaders.Add("clientId", ClientId);
            }
        }

        public virtual HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();

            AddAccessToken(httpClient);

            AddClientId(httpClient);

            AddDeviceId(httpClient);

            return httpClient;
        }
    }
}
