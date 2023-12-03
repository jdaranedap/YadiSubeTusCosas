using ExternalService.DAO.ExternalDTO.SpotifyDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.DAO
{
    public class SpotifyService
    {
        public string clientId = "4dc7eac21d584910b0339f149bff3e9e";
        public string clientSecret = "a6aa8fbdfb0d4590baa827aa4548ebf6";
        public string authorizationEndpoint = "https://accounts.spotify.com/authorize";
        public string tokenEndpoint = "https://accounts.spotify.com/api/token";
        public async Task<TopArtistsEDTO> GetTopArtistsByUserAsync(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/top/artists");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            TopArtistsEDTO result = JsonConvert.DeserializeObject<TopArtistsEDTO>(await response.Content.ReadAsStringAsync());
            return result;
        }
        public async Task<TopTracksArtistEDTO> GetTopTracksByArtist(string artistHref, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{artistHref}/top-tracks?market=ES");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var contentResponse = await response.Content.ReadAsStringAsync();
            TopTracksArtistEDTO result = JsonConvert.DeserializeObject<TopTracksArtistEDTO>(contentResponse);
            return result;
        }
        public string GetToken()
        {
            using (var client = new HttpClient())
            {
                var requestBody = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
                var response = client.PostAsync(tokenEndpoint, requestBody).Result;

                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                var tokenResult = JObject.Parse(json)["access_token"].Value<string>();
                return tokenResult;
            }
        }
    }

    
}
