using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Controllers
{
    public class DemoController : ControllerBase
    {
        // Get /status
        //[HttpGet("/status")]
        //public string GetTheStatus()
        //{
        //    return "Everything looks good.";
        //}
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

        // GET /products/<some number/id>
        [HttpGet("/products/{productId:int}")]
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
