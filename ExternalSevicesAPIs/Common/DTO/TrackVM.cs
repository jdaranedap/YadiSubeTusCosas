using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class TrackVM
    {
        public string name { get; set; }
        public string album_name { get; set; }
        public string external_url { get; set; }
        public ImagesVM album_image { get; set; }
    }
}
