using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KursRozkladController : ControllerBase
    {
        private readonly IKursRozkladService _kursRozkladService;

        public KursRozkladController(IKursRozkladService kursRozkladService)
        {
            _kursRozkladService = kursRozkladService;
        }

        [HttpGet("{id}")]
        public ActionResult<Dictionary<string, string>> Get(int id)
        {
            try 
            {
                var result = _kursRozkladService.GetKursIdAndRozkladOcen(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Wystąpił błąd podczas próby pobrania danych. Proszę spróbować ponownie później.");
            }
        }
    }
}
