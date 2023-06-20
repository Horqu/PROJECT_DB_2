using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

[Route("api/[controller]")]
[ApiController]
public class KursController : ControllerBase
{
    private readonly KursService _kursService;

    public KursController(KursService kursService)
    {
        _kursService = kursService;
    }

    [HttpGet("{kursId}")]
    public ActionResult<Dictionary<string, string>> GetCzestoscPiatki(int kursId)
    {
        try
        {
            var result = _kursService.GetCzestoscPiatkiForKursId(kursId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Prawdopodobnie chcesz zalogować wyjątek tutaj

            return StatusCode(500, "Wystąpił błąd podczas próby pobrania danych. Proszę spróbować ponownie później.");
        }
    }
}
