using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SongsAPI;
using SongsAPI.Controllers;
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
        //overriding the configure serivces to inject our test dummy
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Has alredy ran the production ConfigureServices
                //ask for the service that is implmentation of IProvideServerStatus
                //and remove it

                var serviceDescription = services.Single(service =>
                    service.ServiceType == typeof(IProvideServerStatus));
                //removing it
                services.Remove(serviceDescription);
                //replace it
                services.AddScoped<IProvideServerStatus, DummyService>();
            });
        }

        public class DummyService : IProvideServerStatus
        {
            public SongsAPI.Controllers.GetStatusResponse GetMyStatus()
            {
                return new SongsAPI.Controllers.GetStatusResponse
                {
                    Message = "Dummy says Howdy!",
                    LastChecked = new DateTime(1969, 4, 20, 23, 59, 00)
                };
            }
        }
    }
}
