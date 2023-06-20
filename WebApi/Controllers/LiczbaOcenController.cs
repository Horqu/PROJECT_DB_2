using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiczbaOcenController : ControllerBase
    {
        private readonly ILiczbaOcenService _service;

        public LiczbaOcenController(ILiczbaOcenService service)
        {
            _service = service;
        }

        [HttpGet("{studentId}/{startDate}/{endDate}")]
        public ActionResult<string> Get(int studentId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(_service.GetLiczbaOcen(startDate, endDate, studentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
