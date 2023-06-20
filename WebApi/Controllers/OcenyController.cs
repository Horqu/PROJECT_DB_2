using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcenyController : ControllerBase
    {
        private readonly IOcenaService _service;

        public OcenyController(IOcenaService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<Ocena> Post([FromBody] Ocena nowaOcena)
        {
            try
            {
                _service.DodajOcene(nowaOcena);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Ocena>> Get()
        {
            try
            {
                return Ok(_service.GetOceny());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
