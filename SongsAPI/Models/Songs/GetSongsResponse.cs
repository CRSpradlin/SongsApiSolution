using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Models.Songs
{
    //Created this representation in JSON first, pasted it as JSON and edited some uppercasings and changed the array of data to a List for ease of use
    public class GetSongsResponse
    {
        public List<SongSummaryItem> Data { get; set; }
    }

    public class SongSummaryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string RecommendedBy { get; set; }
    }
}
