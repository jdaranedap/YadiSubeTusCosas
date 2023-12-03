using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.DAO.ExternalDTO.SpotifyDTO
{
    public class ArtistEDTO : ArtistBaseEDTO
    {
        public int popularity { get; set; }
        public List<string> genres { get; set; }
        public List<ImagesEDTO> images { get; set; }
    }
}
