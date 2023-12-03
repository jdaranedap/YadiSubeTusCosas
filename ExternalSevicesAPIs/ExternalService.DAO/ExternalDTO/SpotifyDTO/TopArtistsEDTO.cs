using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.DAO.ExternalDTO.SpotifyDTO
{
    public class TopArtistsEDTO
    {
        public List<ArtistEDTO> items { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public string href { get; set; }
        public string next { get; set; }
        public string previous { get; set; }       

    }
}
