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

        [HttpPost("/songs")]
        public async Task<ActionResult> AddASong([FromBody] PostSongRequest request)
        {
            // 1. Validate the Entity (Title is Required, but Artist isn't)
            //      - If not valid, send a 400 with or without some details about what they did wrong.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // 2. Modify the domain - save it to the database.
            var song = new Song
            {
                Title = request.Title,
                Artist = request.Artist,
                RecommendedBy = request.RecommendedBy,
                IsActive = true,
                AddedToInventory = DateTime.Now
            };
            _context.Songs.Add(song);
            //Can add multiple songs here
            await _context.SaveChangesAsync();
            // 3. Return:
            //      - 201 Created Status Code
            //      - Give them a copy of the newly created resource.
            //      - Add a location header with the URL of the newly created resource. -- For caching purposes
            //          E.g. Location: http://localhost:1337/songs/5
            // Fluent Validation can help with automatic API validation.
            var response = new GetASongResponse
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                RecommendedBy = song.RecommendedBy
            };
            return CreatedAtRoute("songs#getasong", new { id = response.Id }, response);
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

        [HttpGet("/songs/{id:int}", Name = "songs#getasong")]
        public async Task<ActionResult> GetASong(int id)
        {
            var response = await _context.Songs
                .Where(s => s.IsActive && s.Id == id)
                .Select(s => new GetASongResponse
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    RecommendedBy = s.RecommendedBy
                })
                .SingleOrDefaultAsync();

            if(response == null)
            {
                return NotFound();
            } else
            {
                return Ok(response);
            }
        }
    }
}
