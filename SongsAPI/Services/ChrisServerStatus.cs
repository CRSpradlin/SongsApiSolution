using SongsAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Services
{
    public class ChrisServerStatus : IProvideServerStatus
    {
        public GetStatusResponse GetMyStatus()
        {
            return new GetStatusResponse
            {
                Message = "Everything is operational.",
                LastChecked = DateTime.Now
            };
        }
    }
}
