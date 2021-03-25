using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAPI.Domain;
using SongsAPI.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Controllers
{
    public class SongsController : ControllerBase
    {
        private SongsDataContext _context;

        public SongsController(SongsDataContext context)
        {
            _context = context;
        }

        [HttpGet("/songs")]
        public async Task<ActionResult> GetAllSongs()
        {
            var response = new GetSongsResponse();

            var data = await _context.Songs
                .Where(song => song.IsActive == true)
                .Select(song => new SongSummaryItem //Created SongSummaryItem for every song to create the correct data type to form
                {
                    Id = song.Id,
                    Title = song.Title,
                    Artist = song.Artist,
                    RecommendedBy = song.RecommendedBy
                })
                .OrderBy(song => song.Title)
                .ToListAsync();

            //Can't just do this without having the .Select with the query above
            response.Data = data;



            return Ok(response);
        }
    }
}
