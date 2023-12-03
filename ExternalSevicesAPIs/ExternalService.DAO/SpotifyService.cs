using ExternalService.DAO.ExternalDTO.SpotifyDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExternalService.DAO
{
    public class SpotifyService
    {
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
    }

    
}
