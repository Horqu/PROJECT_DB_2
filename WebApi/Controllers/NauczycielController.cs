using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NauczycielController : ControllerBase
    {
        private readonly INauczycielService _nauczycielService;

        public NauczycielController(INauczycielService nauczycielService)
        {
            _nauczycielService = nauczycielService;
        }

        [HttpGet("{id}")]
        public ActionResult<Dictionary<string, string>> Get(int id)
        {
            try
            {
                var result = _nauczycielService.GetNauczycielIdAndSredniaOcen(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Prawdopodobnie chcesz zalogować wyjątek tutaj

                return StatusCode(500, "Wystąpił błąd podczas próby pobrania danych. Proszę spróbować ponownie później.");
            }
        }
    }
}
