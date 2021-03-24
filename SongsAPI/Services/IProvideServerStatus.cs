using SongsAPI.Controllers;

namespace SongsAPI
{
    public interface IProvideServerStatus
    {
        GetStatusResponse GetMyStatus();
    }
}