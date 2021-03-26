using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAPI.Domain;
using SongsAPI.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace SongsAPI.Controllers
{
    public class SongsController : ControllerBase
    {
        private SongsDataContext _context;
        private IMapper _mapper;
        private MapperConfiguration _config;

        public SongsController(SongsDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        [HttpDelete("/songs/{id:int}")]
        public async Task<ActionResult> RemoveSong(int id)
        {
            var savedSong = await _context.GetActiveSongs().SingleOrDefaultAsync(s => s.Id == id);

            if(savedSong != null)
            {
                savedSong.IsActive = false;
                await _context.SaveChangesAsync() ;
            }

            // We return no content because returning a 404 is contradictory to removing a resource
            return NoContent();
        }

        [HttpPost("/songs")]
        public async Task<ActionResult> AddASong([FromBody] PostSongRequest request)
        {
            // adding delay to simulate real life
            await Task.Delay(8 * 1000);
            //

            // 1. Validate the Entity (Title is Required, but Artist isn't)
            //      - If not valid, send a 400 with or without some details about what they did wrong.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // 2. Modify the domain - save it to the database.
            var song = _mapper.Map<Song>(request);
            //var song = new Song
            //{
            //    Title = request.Title,
            //    Artist = request.Artist,
            //    RecommendedBy = request.RecommendedBy,
            //    IsActive = true,
            //    AddedToInventory = DateTime.Now
            //};
            _context.Songs.Add(song);
            //Can add multiple songs here
            await _context.SaveChangesAsync();
            // 3. Return:
            //      - 201 Created Status Code
            //      - Give them a copy of the newly created resource.
            //      - Add a location header with the URL of the newly created resource. -- For caching purposes
            //          E.g. Location: http://localhost:1337/songs/5
            // Fluent Validation can help with automatic API validation.
            var response = _mapper.Map<GetASongResponse>(song);
            //var response = new GetASongResponse
            //{
            //    Id = song.Id,
            //    Title = song.Title,
            //    Artist = song.Artist,
            //    RecommendedBy = song.RecommendedBy
            //};
            return CreatedAtRoute("songs#getasong", new { id = response.Id }, response);
        }

        [HttpGet("/songs")]
        public async Task<ActionResult> GetAllSongs()
        {
            // Adding a delay to simulate real life 
            await Task.Delay(8 * 1000);
            //

            var response = new GetSongsResponse();

            var data = await _context.GetActiveSongs()
                .ProjectTo<SongSummaryItem>(_config)
                .OrderBy(song => song.Title)
                .ToListAsync();

            //Can't just do this without having the .Select with the query above
            response.Data = data;



            return Ok(response);
        }

        [HttpGet("/songs/{id:int}", Name = "songs#getasong")]
        public async Task<ActionResult> GetASong(int id)
        {
            var response = await _context.GetActiveSongs()
                .Where(s => s.Id == id)
                .ProjectTo<GetASongResponse>(_config)
                //.Select(s => new GetASongResponse
                //{
                //    Id = s.Id,
                //    Title = s.Title,
                //    Artist = s.Artist,
                //    RecommendedBy = s.RecommendedBy
                //})
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
