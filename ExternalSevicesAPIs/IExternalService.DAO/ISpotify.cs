using Common.DTO;
namespace IExternalService.DAO
{
    public interface ISpotify
    {
        List<ArtistVM> GetTopArtists(string token);
        List<TrackVM> GetTracksByArtist(string hrefArtist);
    }
}