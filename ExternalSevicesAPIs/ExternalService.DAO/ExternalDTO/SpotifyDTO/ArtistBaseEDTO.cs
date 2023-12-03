using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.DAO.ExternalDTO.SpotifyDTO
{
    public class ArtistBaseEDTO
    {
        public string href {  get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public ExternalUrlsEDTO external_urls { get; set; }
    }    
}
