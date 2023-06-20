using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiczbaOcenPozNegController : ControllerBase
    {
        private readonly ILiczbaOcenPozNegService _service;

        public LiczbaOcenPozNegController(ILiczbaOcenPozNegService service)
        {
            _service = service;
        }

        [HttpGet("{kursId}")]
        public ActionResult<string> Get(int kursId)
        {
            try
            {
                return Ok(_service.GetLiczbaOcenPozNeg(kursId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd podczas próby pobrania danych. Proszę spróbować ponownie później.");
            }
        }
    }
}
