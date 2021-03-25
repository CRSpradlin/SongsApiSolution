using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Models.Songs
{
    public class GetASongResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string RecommendedBy { get; set; }
    }
}
