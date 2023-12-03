using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.DAO.ExternalDTO.SpotifyDTO
{
    public class TracksEDTO
    {
        public string href { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public bool is_playable { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public ExternalUrlsEDTO external_urls { get; set; }
        public List<ArtistBaseEDTO> artists { get; set; }
        public AlbumEDTO album { get; set; }
    }
}
