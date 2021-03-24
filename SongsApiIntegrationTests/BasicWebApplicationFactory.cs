using Microsoft.AspNetCore.Mvc.Testing;
using SongsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsApiIntegrationTests
{
    // This runs your test on a faked hosted api, hense why we passed in Startup
    public class BasicWebApplicationFactory : WebApplicationFactory<Startup>
    {

    }
}
