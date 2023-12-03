using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ArtistVM
    {
        public string name { get; set; }        
        public string href_artist { get; set; }
        public string external_url { get; set; }
        public ImagesVM artist_image { get; set; }
    }
}
