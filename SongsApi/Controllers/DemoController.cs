using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Controllers
{
    public class DemoController : ControllerBase
    {
        // GET /status
        [HttpGet("/status")]
        public ActionResult<GetStatusResponse> GetTheStatus()
        {

            var response = new GetStatusResponse
            {
                Message = "Everything is operational.",
                LastChecked = DateTime.Now
            };
            return Ok(response);
        }
        // GET /products/38938983983 (Route Param)
        [HttpGet("/products/{productId:int}")] // Route Constraints.
        public ActionResult LookupProduct(int productId)
        {
            return Ok("Information about product: " + productId);
        }
    }

    public class GetStatusResponse
    {
        public string Message { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
