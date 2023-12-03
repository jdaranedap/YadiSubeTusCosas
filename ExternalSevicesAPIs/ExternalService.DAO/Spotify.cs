using IExternalService.DAO;
using ExternalService.DAO.ExternalDTO.SpotifyDTO;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Common.DTO;
using System.Collections.Generic;

namespace ExternalService.DAO
{
    public class Spotify : ISpotify
    {        
        private readonly SpotifyService spotifyServiceConnect;
        public Spotify() 
        {
            spotifyServiceConnect = new SpotifyService();
        }
        public List<ArtistVM> GetTopArtists(string token)
        {
            var resultTopArtist = spotifyServiceConnect.GetTopArtistsByUserAsync(token);
            var result = ParseToArtistVM(resultTopArtist.Result);
            return result;
        }
        public List<TrackVM> GetTracksByArtist(string token,string hrefArtist)
        {
            var resultTopTracks = spotifyServiceConnect.GetTopTracksByArtist(hrefArtist, token);
            var result = ParseToTrackVM(resultTopTracks.Result);
            return result;
        }
        private List<ArtistVM> ParseToArtistVM(TopArtistsEDTO topArtistresult)
        {
            List<ArtistVM> result = new List<ArtistVM>();
            foreach (var top in topArtistresult.items)
            {
                var itemImage = top.images.FirstOrDefault();
                ArtistVM artistConvert = new ArtistVM
                {
                    name = top.name,
                    external_url= top.external_urls.spotify,
                    href_artist = top.href,
                    artist_image = new ImagesVM { 
                        height = itemImage.height,
                        width = itemImage.width,
                        url = itemImage.url,
                    }
                };
                result.Add(artistConvert);
            }
            return result;
        }
        private List<TrackVM> ParseToTrackVM(TopTracksArtistEDTO topTracksArtist)
        {
            List<TrackVM> result = new List<TrackVM>();
            foreach(var top in topTracksArtist.tracks)
            {
                var itemImage = top.album.images.FirstOrDefault();
                TrackVM trackConvert = new TrackVM
                {
                    name = top.name,
                    album_name = top.album.name,
                    external_url = top.external_urls.spotify,
                    album_image = new ImagesVM
                    {
                        height = itemImage.height,
                        width = itemImage.width,
                        url = itemImage.url,
                    }
                };
                result.Add(trackConvert);
            }
            return result;
        }
    }
}