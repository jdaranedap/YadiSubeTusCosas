using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.DAO.ExternalDTO.SpotifyDTO
{
    public class AlbumEDTO
    {
        public string name { get; set; }
        public string release_date { get; set; }
        public List<ImagesEDTO> images { get; set; }
        public ExternalUrlsEDTO external_urls { get; set; }
    }
}
