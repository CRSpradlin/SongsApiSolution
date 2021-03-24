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

        // GET /employees
        // GET /employees?department=QA
        [HttpGet("/employees")]
        public ActionResult GetEmployees([FromQuery] string department = "All")
        {
            return Ok("Getting Employees Collection (" + department + ")");
        }

        [HttpPost("/employees")]
        public ActionResult HireAnEmployee([FromBody] PostEmployeeRequest employeeToHire)
        {
            return Ok($"Hiring {employeeToHire.LastName} in the {employeeToHire.Department} dept.");
        }

        [HttpGet("/whoami")]
        public ActionResult WhoAmi([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }
    }

    public class GetStatusResponse
    {
        public string Message { get; set; }
        public DateTime LastChecked { get; set; }

    }

    public class PostEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }

}
