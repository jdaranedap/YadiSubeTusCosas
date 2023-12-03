using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;
using WebApplicationPOC.Models;
using ExternalService.DAO;

namespace WebApplicationPOC.Controllers
{
    public class HomeController : Controller
    {
        public string clientId = "4dc7eac21d584910b0339f149bff3e9e";
        public string clientSecret = "a6aa8fbdfb0d4590baa827aa4548ebf6";
        public string redirectUri = "https://localhost:7148/home/callback";
        public string authorizationEndpoint = "https://accounts.spotify.com/authorize";
        public string tokenEndpoint = "https://accounts.spotify.com/api/token";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var redirectsUri = Uri.EscapeDataString(redirectUri);

            var authorizationUrl = $"{authorizationEndpoint}?" +
                $"response_type=code&client_id={clientId}&scope=user-follow-read%20user-top-read%20user-read-recently-played%20user-library-read&redirect_uri={redirectsUri}";

            return Redirect(authorizationUrl);
        }

        public async Task<IActionResult> Callback(string code)
        {
            var token = GetTokenAuth(code);
            
            // Pasar el token a la vista utilizando ViewBag
            ViewBag.Token = token;
            // Devolver la vista con el token
            return View("Privacy");

            //var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);

            //var credentials = $"{clientId}:{clientSecret}";
            //var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));

            //tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            //tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            //{
            //    { "code", code },
            //    { "redirect_uri", redirectUri },
            //    { "grant_type", "authorization_code" }
            //});

            //using (var client = new HttpClient())
            //{
            //    var response = await client.SendAsync(tokenRequest);

            //    response.EnsureSuccessStatusCode();

            //    var json = await response.Content.ReadAsStringAsync();
            //    var token = JObject.Parse(json)["access_token"].Value<string>();

            //    var uri = new Uri($"http://localhost:3000/#{Uri.EscapeDataString(token)}");
            //    return Redirect(uri.ToString());
            //}
        }
        public string GetTokenAuth(string code)
        {
            string tokenResult = string.Empty;
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);

            var credentials = $"{clientId}:{clientSecret}";
            var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));

            tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            });

            using (var client = new HttpClient())
            {
                var response = client.SendAsync(tokenRequest).Result; // Bloquea hasta que se complete la tarea

                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().Result; // Bloquea hasta que se complete la tarea
                tokenResult = JObject.Parse(json)["access_token"].Value<string>();
            }
            return tokenResult;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}