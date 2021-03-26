using AutoMapper;
using SongsAPI.Domain;
using SongsAPI.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Profiles
{
    public class SongsProfile : Profile
    {
        public SongsProfile()
        {
            CreateMap<Song, GetSongsResponse>();
            CreateMap<Song, SongSummaryItem>();
            CreateMap<Song, GetASongResponse>();
            CreateMap<PostSongRequest, Song>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom((_) => true))
                .ForMember(dest => dest.AddedToInventory, opt => opt.MapFrom(_ => DateTime.Now)); //The underscore is the source, so you could manipulate it.
        }

    }
}
