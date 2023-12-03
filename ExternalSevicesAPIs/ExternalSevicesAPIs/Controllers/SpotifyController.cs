using Common.DTO;
using ExternalService.DAO;
using IExternalService.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExternalSevicesAPIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyController : ControllerBase
    {        
        private readonly ILogger<SpotifyController> _logger;
        private readonly ISpotify spotifyDAO;

        public SpotifyController(ILogger<SpotifyController> logger, ISpotify dao)
        {
            _logger = logger;
            spotifyDAO = dao;
        }

        [HttpGet(Name = "GetTopArtistByUser")]
        public IEnumerable<ArtistVM> GetArtists(string token)
        {
            var result = spotifyDAO.GetTopArtists(token);
            return result;
        }
        [HttpGet("GetTopTracks", Name = "GetTopTracksByUser")]
        public IEnumerable<TrackVM> GetTopTracks(string token, string hrefArtist)
        {
            var result = spotifyDAO.GetTracksByArtist(token, hrefArtist);
            return result;
        }

    }
}