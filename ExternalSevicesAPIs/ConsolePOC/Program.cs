using ExternalService.DAO;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


string ClientId = "4dc7eac21d584910b0339f149bff3e9e";
string ClientSecret = "a6aa8fbdfb0d4590baa827aa4548ebf6";
string RedirectUri = "http://localhost:8888/callback";
string AuthorizationEndpoint = "https://accounts.spotify.com/authorize";
string TokenEndpoint = "https://accounts.spotify.com/api/token";
string token = string.Empty;
var redirectUri = Uri.EscapeDataString(RedirectUri);

var authorizationUrl = $"{AuthorizationEndpoint}?" +
    $"response_type=code&client_id={ClientId}&scope=user-read-private%20user-read-email&redirect_uri={redirectUri}";

Console.WriteLine($"Visit the following URL to authorize the application:\n{authorizationUrl}");
Console.Write("Enter the code from the callback URL: ");
var code = Console.ReadLine();

var tokenRequest = new HttpRequestMessage(HttpMethod.Post, TokenEndpoint)
{
    Content = new FormUrlEncodedContent(new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>
            {
                new System.Collections.Generic.KeyValuePair<string, string>("code", code),
                new System.Collections.Generic.KeyValuePair<string, string>("redirect_uri", RedirectUri),
                new System.Collections.Generic.KeyValuePair<string, string>("grant_type", "authorization_code")
            })
};

using (var client = new HttpClient())
{
    var credentials = $"{ClientId}:{ClientSecret}";
    var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));
    tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

    var response = await client.SendAsync(tokenRequest);

    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    token = Newtonsoft.Json.Linq.JObject.Parse(json)["access_token"].Value<string>();

    Console.WriteLine($"Access Token: {token}");
}



Console.WriteLine("Hello, World!");

Spotify spotifyDAO = new Spotify();
var resultTopArtist = spotifyDAO.GetTopArtistsByUserAsync(token);
var topOneArtist = resultTopArtist.Result.items.FirstOrDefault();
var resultTopTracks = spotifyDAO.GetTopTracksByArtist(topOneArtist.href,token);
var topTracksByArtists = resultTopArtist.Result;